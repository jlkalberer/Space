using System;
using System.Linq;
using Space.DTO;
using Space.DTO.Spatial;
using Space.Repository;
using System.Collections.Generic;
using Space.Repository.Entities;

namespace Space.Game
{
    public class Game
    {
        public const int Width = 120;
        public const int Height = 120;

        private readonly IPlayerRepository _playerRepository;
        private readonly ISolarSystemRepository _solarSystemRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IConstantsProvider _constantsProvider;

        public Game(IPlayerRepository playerRepository, ISolarSystemRepository solarSystemRepository,
            IPlanetRepository planetRepository, IConstantsProvider constantsProvider)
        {
            _playerRepository = playerRepository;
            _solarSystemRepository = solarSystemRepository;
            _planetRepository = planetRepository;
            _constantsProvider = constantsProvider;
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
                            });

            _playerRepository.SaveChanges();
        }

        public void GenerateGalaxy()
        {
            var solarSystems = new List<SolarSystem>();
            var r = new Random();

            for(var i = 0; i < Width; i += 1)
            {
                for(var j = 0; j < Height; j += 1)
                {
                    if (r.NextDouble() > .9)
                    {
                        // create a solar system here...
                        var solarSystem = new SolarSystem();
                        solarSystem.Latitude = i;
                        solarSystem.Longitude = j;

                        // randomly create star -- some solar systems will not have stars but instead, large gas giants
                        solarSystem.SpatialEntities = new List<SpatialEntity>();
                        var v = new SpatialEntity();
                    }
                }
            }
        }

        private void CreateSolarSystem()
        {
            var solarSystem = _solarSystemRepository.Create();
            
            // get the type of solar system -- what type of central mass it has
            var r = new Random();
            var ssc = new SolarSystemConstants(_constantsProvider);

            var numberOfEntities = r.Next(ssc.MinimumEntities, ssc.MaximumEntities);

            // Create this array so we can use it to decide which entity is to be created
            var probabilityArray = (from e in Enum.GetValues(typeof (SpatialEntityType)).Cast<SpatialEntityType>()
                                    select new KeyValuePair<float, SpatialEntityType>(
                                        ssc.SpawningProbability(e), e)).ToList();
                                    
            // Get the sum of the probability array
            var sum = probabilityArray.Select(o => o.Key).Sum();

            for(var i = 0; i < numberOfEntities; i += 1)
            {
                SpatialEntityType itemType;
                var value = r.NextDouble()*sum;
                var count = 0.0f;
                
                // Using the randomly generated value, get the item type from the array
                for(var j = 0; j < probabilityArray.Count; j += 1)
                {
                    count += probabilityArray[j].Key;
                    if (value < count)
                    {
                        itemType = probabilityArray[j].Value;
                    }
                }

                // now create the entity!!!
                
            }
        }
    }
}
