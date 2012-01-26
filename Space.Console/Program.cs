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
            var solarSystemRepository = kernel.Get<ISolarSystemRepository>();

            var game = kernel.Get<Game.Game>();

            var player = CreatePlayer(playerRepository);
            player = playerRepository.Get(player.ID);
            
            //var solarSystem = solarSystemRepository.Create();
            //solarSystemRepository.SaveChanges();

            //CreatePlanet(planetRepository, player, solarSystem.ID);
            var galaxy = game.GenerateGalaxy();
            solarSystemRepository.SaveChanges();

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

        private static void RenderGalaxy(IEnumerable<SolarSystem> ss)
        {
            for (var i = 0; i < Game.Game.Width; i += 1)
            {
                for(var j = 0; j < Game.Game.Height; j += 1)
                {
                    System.Console.Write(ss.Any(s => (int)s.Latitude == i && (int)s.Longitude == j) ? "x " : "· ");
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
