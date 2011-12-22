using System;
using System.Linq;
using Space.DTO;
using Space.Repository;

namespace Space.Game
{
    public class Game
    {
        public const int Width = 12;
        public const int Height = 12;

        private readonly IPlayerRepository _playerRepository;
        private readonly IPlanetRepository _planetRepository;

        public Game(IPlayerRepository playerRepository, 
            IPlanetRepository planetRepository)
        {
            _playerRepository = playerRepository;
            _planetRepository = planetRepository;
        }

        /// <summary>
        /// The main update that happens each tick in the game
        /// 1. Explore
        /// 2. Update fleet position/run battle
        /// 3. Update planets
        /// 4. 
        /// </summary>
        public void Update()
        {
            var planetsToUpdate = _planetRepository.All.Where(p => p.Owner != null).GroupBy(p => p.Owner.ID);

            planetsToUpdate
                .AsParallel()
                .ForAll(planetSet =>
                            {
                                var netTotalValue = new NetValue();
                                var user = _playerRepository.Get(planetSet.Key);
                                if (user == null)
                                {
                                    return;
                                }

                                foreach (var planet in planetSet)
                                {
                                    var netValue = planet.Update(user.Bonuses);
                                    netTotalValue.Add(netValue);
                                    _planetRepository.Update(planet);
                                }

                                user.Update(netTotalValue);
                                //_playerRepository.Update(user);
                            });
            _playerRepository.SaveChanges();
        }

        public bool[] GalaxyGrid { get; set; }

        public void GenerateGalaxy()
        {
            GalaxyGrid = new bool[Width * Height];

            var r = new Random();

            for(var i = 0; i < Width; i += 1)
            {
                for(var j = 0; j < Height; j += 1)
                {
                    GalaxyGrid[i * Width + j] = r.NextDouble() > .9;
                }
            }
        }
    }
}
