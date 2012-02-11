using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Space.DTO;
using Space.DTO.Spatial;
using Space.Repository;
using Space.Repository.EF;

namespace Space.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new INinjectModule[] {new EFModule(), new ConsoleModule()});
            var db = kernel.Get<EFDBContext>();
            db.Database.Delete(); // delete the database each time we run so we can start from scratch
            db.Database.Create();

            var playerRepository = kernel.Get<IPlayerRepository>();
            var galaxyRepository = kernel.Get<IGalaxyRepository>();

            var game = kernel.Get<Game.Game>();

            var player = CreatePlayer(playerRepository);
            player = playerRepository.Get(player.ID);
            
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
                        RenderGalaxy(galaxy);
                        break;
                    case ConsoleKey.T:
                        game.Update();
                        break;
                    case ConsoleKey.P:
                        PrintPlayerStats(playerRepository, player);
                        break;
                }

            } while (ki.Key != ConsoleKey.Escape);
        }

        private static void RenderGalaxy(Galaxy galaxy)
        {
            for (var i = 0; i < galaxy.GalaxySettings.Width; i += 1)
            {
                for (var j = 0; j < galaxy.GalaxySettings.Height; j += 1)
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
            player.Race = new Race();
            playerRepository.SaveChanges();

            return player;
        }

        private static void PrintPlayerStats(IPlayerRepository playerRepository, Player player)
        {
            player = playerRepository.Get(player.ID);
            System.Console.WriteLine("Cash - ${0}", player.TotalNetValue.Cash);
            System.Console.WriteLine("Food - {0}", player.TotalNetValue.Food);
            System.Console.WriteLine("Population - {0}", player.TotalNetValue.Population);
        }
    }
}
