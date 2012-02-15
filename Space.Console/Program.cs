﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Space.DTO;
using Space.DTO.Buildings;
using Space.DTO.Players;
using Space.DTO.Spatial;
using Space.Infrastructure;
using Space.Repository;
using Space.Repository.EF;

namespace Space.Console
{
    class Program
    {
        static void Setup()
        {
            System.Console.SetWindowSize(System.Console.WindowWidth*2, System.Console.WindowHeight);
        }
        static void Main(string[] args)
        {
            Setup();

            IKernel kernel = new StandardKernel(new INinjectModule[] {new EFModule(), new ConsoleModule()});
            var db = kernel.Get<EFDBContext>();
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
                ss.SpatialEntities.Where(e => e is Planet);
            var planet = se.ElementAt(r.Next(se.Count() - 1)) as Planet;

            if (planet != null)
            {
                planet.Owner = player;
                playerRepository.SaveChanges();
            }

            ConsoleKeyInfo ki;
            do
            {
                System.Console.WriteLine("\r\n******");
                System.Console.WriteLine("g -- render galaxy");
                System.Console.WriteLine("c -- create player");
                System.Console.WriteLine("p -- print player stats");   
                System.Console.WriteLine("t -- run tick");
                System.Console.WriteLine("******");
                
                ki = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (ki.Key)
                {
                    case ConsoleKey.G:
                        RenderGalaxy(galaxy, null);
                        break;
                    case ConsoleKey.T:
                        game.Update();
                        break;
                    case ConsoleKey.P:
                        PrintPlayerStats(playerRepository, player);
                        break;
                        case ConsoleKey.B:
                        {
                            var planetToBuildOn = SelectPlanet(player.Planets);
                            if(planetToBuildOn == null)
                            {
                                break;
                            }

                        }
                        break;
                }

            } while (ki.Key != ConsoleKey.Escape);
        }

        private static void RenderGalaxy(Galaxy galaxy, GalaxySettings settings)
        {
            for (var i = 0; i < settings.Width; i += 1)
            {
                for (var j = 0; j < settings.Height; j += 1)
                {
                    System.Console.Write(galaxy.SolarSystems.Any(s => (int)s.Latitude == i && (int)s.Longitude == j) ? "x " : "· ");
                }
                System.Console.WriteLine();
            }
        }

        private static Player CreatePlayer(IPlayerRepository playerRepository)
        {
            var player = playerRepository.Create();
            player.TotalNetValue = new NetValue();
            player.TotalNetValue.Iron = player.ID;
            player.ResearchPoints = new ResearchPoints();
            player.ResearchPoints.PlayerID = player.ID;
            player.TickValue = new TickValue();
            player.TickValue.PlayerID = player.ID;
            player.Race = new Race();

            return player;
        }

        private static Planet SelectPlanet(ICollection<Planet> planets)
        {
            Planet output = null;

            System.Console.WriteLine("\r\n******");
            System.Console.WriteLine("Select a planet from the list:");
            for (var i = 0; i < planets.Count; i += 1 )
            {
                var p = planets.ElementAt(i);
                var ss = p.SolarSystem;                
                System.Console.WriteLine(string.Format("{0} -- System: {1},{2} Planet: {3} ", i, ss.Latitude, ss.Longitude, p.PlanetNumber));
            }
            System.Console.WriteLine("******");


            var input = System.Console.ReadLine();
            int planetNumber;
            if(int.TryParse(input, out planetNumber))
            {
                output = planets.ElementAt(planetNumber);
            }

            return output;
        }

        private static void BuildBuildings(Player player, Planet planet)
        {
            do
            {
                System.Console.WriteLine("\r\n******");
                System.Console.WriteLine("Select building type");
                
                var buildingTypes = SystemTypes.EnumToList<BuildingType>();
                for (var i = 0; i < buildingTypes.Count(); i += 1)
                {
                    System.Console.WriteLine("{0} - {1}", i, buildingTypes.ElementAt(i));
                }

                System.Console.WriteLine("Escape -- Finished building");
                System.Console.WriteLine("******");

                var input = System.Console.ReadLine();
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
                System.Console.WriteLine("Number of buildings:");

                input = System.Console.ReadLine();
                int buildingCount;
                if (!int.TryParse(input, out buildingCount))
                {
                    continue;
                }

                // build buildings on planet.

            } while (true);
        }

        private static void PrintPlayerStats(IPlayerRepository playerRepository, Player player)
        {
            System.Console.WriteLine("\r\n*********");
            player.TotalNetValue.Print();
            System.Console.WriteLine("******");
            player.TickValue.Print();
            System.Console.WriteLine("*********\r\n");
            //player = playerRepository.Get(player.ID);
            //System.Console.WriteLine("Cash - ${0}", player.TotalNetValue.Cash);
            //System.Console.WriteLine("Food - {0}", player.TotalNetValue.Food);
            //System.Console.WriteLine("Population - {0}", player.TotalNetValue.Population);
        }
    }
}
