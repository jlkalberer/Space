// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The Program to run the console version of the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Console
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Ninject;
    using Ninject.Modules;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;
    using Space.Game;
    using Space.Infrastructure;
    using Space.Repository;
    using Space.Repository.EF;
    using Space.Scheduler;
    using Space.Scheduler.Quartz;

    /// <summary>
    /// The Program to run the console version of the game.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the console program.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>        public static void Main(string[] args)
        {
            Setup();

            var kernel = new StandardKernel(new INinjectModule[]
                {
                    new EntityFrameworkModule(), 
                    new ConsoleModule(),
                    new QuartzModule()
                });

            var initializer = kernel.Get<IDatabaseInitializer<EntityFrameworkDbContext>>();
            Database.SetInitializer(initializer);

            var playerRepository = kernel.Get<IPlayerRepository>();
            var galaxyRepository = kernel.Get<IGalaxyRepository>();

            var scheduler = kernel.Get<ISpaceScheduler>();

            var game = kernel.Get<Game>();

            var player = CreatePlayer(playerRepository);

            // These settings will be loaded from some default values + user created values.
            var galaxy = game.GenerateGalaxy(null);
            galaxy.Players = new List<Player>
                                 {
                                     player
                                 };
            player.Galaxy = galaxy;

            galaxyRepository.SaveChanges();

            var r = new Random();
            var ss = galaxy.SolarSystems.ElementAt(r.Next(galaxy.SolarSystems.Count - 1));
            var se =
                ss.SpatialEntities.Where(e => e is Planet).ToList();
            var planet = se.ElementAt(r.Next(se.Count() - 1)) as Planet;

            if (planet != null)
            {
                planet.Owner = player;
                playerRepository.SaveChanges();
            }

            ConsoleKeyInfo ki;
            do
            {
                Console.WriteLine("\r\n******");
                Console.WriteLine("g -- render galaxy");
                Console.WriteLine("c -- create player");
                Console.WriteLine("b -- build on planet");
                Console.WriteLine("p -- print player stats");   
                Console.WriteLine("t -- run tick");
                Console.WriteLine("******");
                
                ki = Console.ReadKey();
                Console.WriteLine();

                switch (ki.Key)
                {
                    case ConsoleKey.G:
                        galaxy.Print();
                        break;
                    case ConsoleKey.T:
                        game.Update();
                        break;
                    case ConsoleKey.P:
                        player.Print();
                        break;
                        case ConsoleKey.B:
                        {
                            var planetToBuildOn = SelectPlanet(player.Planets);
                            if (planetToBuildOn == null)
                            {
                                break;
                            }

                            BuildBuildings(planetToBuildOn, player, scheduler);
                        }

                        break;
                }
            } 
            while (ki.Key != ConsoleKey.Escape);
        }
        
        /// <summary>
        /// Setup various settings for the console and game..
        /// </summary>
        private static void Setup()
        {
            Console.SetWindowSize(Console.WindowWidth * 2, Console.WindowHeight);
        }

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="playerRepository">
        /// The player repository.
        /// </param>
        /// <returns>
        /// The newly created player object.
        /// </returns>
        private static Player CreatePlayer(IPlayerRepository playerRepository)
        {
            var player = playerRepository.Create();
            player.TotalNetValue = new NetValue { Iron = 20000, Cash = 50000, Energy = 20000, Food = 10000, Mana = 10000 };
            player.ResearchPoints = new ResearchPoints { PlayerID = player.ID };
            player.TickValue = new TickValue { PlayerID = player.ID };
            player.Race = new Race();

            return player;
        }

        /// <summary>
        /// Select a planet from a list of planets.
        /// </summary>
        /// <param name="planets">
        /// The planets.
        /// </param>
        /// <returns>
        /// The selected planet.
        /// </returns>
        private static Planet SelectPlanet(ICollection<Planet> planets)
        {
            Planet output = null;

            Console.WriteLine("\r\n******");
            Console.WriteLine("Select a planet from the list:");
            for (var i = 0; i < planets.Count; i += 1)
            {
                var p = planets.ElementAt(i);
                var ss = p.SolarSystem;                
                Console.WriteLine(string.Format("{0} -- System: {1},{2} Planet: {3} ", i, ss.Latitude, ss.Longitude, p.PlanetNumber));
            }

            Console.WriteLine("******");

            var input = Console.ReadLine();
            int planetNumber;
            if (int.TryParse(input, out planetNumber))
            {
                output = planets.ElementAt(planetNumber);
            }

            return output;
        }

        /// <summary>
        /// Builds buildings on a planet.
        /// </summary>
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="spaceScheduler">
        /// The space Scheduler.
        /// </param>
        private static void BuildBuildings(Planet planet, Player player, ISpaceScheduler spaceScheduler)
        {
            do
            {
                Console.WriteLine("\r\n******");
                Console.WriteLine("Select building type");

                var buildingTypes = SystemTypes.EnumToList<BuildingType>();
                if (buildingTypes == null)
                {
                    return;
                }

                for (var i = 0; i < buildingTypes.Count(); i += 1)
                {
                    Console.WriteLine("{0} - {1}", i, buildingTypes.ElementAt(i));
                }

                Console.WriteLine("-1 -- Finished building");
                Console.WriteLine("******");

                var input = Console.ReadLine();

                int buildingType;
                if (!int.TryParse(input, out buildingType))
                {
                    continue;
                }

                if (buildingType == -1)
                {
                    break;
                }

                // The maximum buildings for the BuildingType.
                var buildType =
                    player.Galaxy.GalaxySettings.BuildingCosts.FirstOrDefault(
                        bc => bc.Type == (BuildingType)buildingType);

                // Get the total value and set the number of buildings to zero.
                var totalValue = new NetValue();
                totalValue.Add(player.TotalNetValue);
                totalValue.EntityCount = 0;

                var buildingCosts = buildType.CalculateBuildCosts(totalValue, planet.TotalBuildings, planet.BuildingCapacity);

                Console.WriteLine("Number of buildings - (max is {0}):", buildingCosts.EntityCount);

                input = Console.ReadLine();
                int buildingCount;
                if (!int.TryParse(input, out buildingCount))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                

                // build buildings on planet.
                if (!spaceScheduler.BuildBuildings(player, planet, buildType, buildingCosts, buildType.Type))
                {
                    Console.WriteLine(
                        "Cannot build {0} buildings.  The maximum is {1}", buildingCount, buildingCosts.EntityCount);
                }
            }
            while (true);
        }
    }
}
