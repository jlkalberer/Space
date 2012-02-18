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
    using System.Linq;

    using Ninject;
    using Ninject.Modules;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;
    using Space.Infrastructure;
    using Space.Repository;
    using Space.Repository.EF;

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
                    new ConsoleModule()
                });
            var db = kernel.Get<EntityFrameworkDbContext>();
            db.Database.Delete(); // delete the database each time we run so we can start from scratch
            db.Database.Create();

            var playerRepository = kernel.Get<IPlayerRepository>();
            var galaxyRepository = kernel.Get<IGalaxyRepository>();

            var game = kernel.Get<Game.Game>();

            var player = CreatePlayer(playerRepository);
            
            // These settings will be loaded from some default values + user created values.
            var galaxySettings = new GalaxySettings();
            var galaxy = game.GenerateGalaxy(galaxySettings);
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

                            BuildBuildings(player, planetToBuildOn);
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
            player.TotalNetValue = new NetValue { Iron = player.ID };
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
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="planet">
        /// The planet.
        /// </param>
        private static void BuildBuildings(Player player, Planet planet)
        {
            do
            {
                Console.WriteLine("\r\n******");
                Console.WriteLine("Select building type");

                var buildingTypes = SystemTypes.EnumToList<BuildingType>() as List<BuildingType>;
                if (buildingTypes == null)
                {
                    return;
                }

                for (var i = 0; i < buildingTypes.Count; i += 1)
                {
                    Console.WriteLine("{0} - {1}", i, buildingTypes.ElementAt(i));
                }

                Console.WriteLine("Escape -- Finished building");
                Console.WriteLine("******");

                var input = Console.ReadLine();
                if (input == ConsoleKey.Escape.ToString())
                {
                    break;
                }

                int buildingType;
                if (!int.TryParse(input, out buildingType))
                {
                    continue;
                }

                // TODO - Get max number of buildings player can build
                Console.WriteLine("Number of buildings:");

                input = Console.ReadLine();
                int buildingCount;
                if (!int.TryParse(input, out buildingCount))
                {
                    continue;
                }

                // build buildings on planet.
                planet.FarmCount = 100;
            }
            while (true);
        }
    }
}
