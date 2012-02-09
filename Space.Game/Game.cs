using System;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using Space.DTO;
using Space.DTO.Spatial;
using Space.Repository;
using System.Collections.Generic;
using Space.Repository.Entities;

namespace Space.Game
{
    public class Game
    {
        public const int Width = 35;
        public const int Height = 35;
        public const float SystemGenerationProbability = 0.94f;

        private readonly IPlayerRepository _playerRepository;
        private readonly ISolarSystemRepository _solarSystemRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IConstantsProvider _constantsProvider;

        public Game(IPlayerRepository playerRepository, ISolarSystemRepository solarSystemRepository,
            IPlanetRepository planetRepository, IEntityRepository entityRepository, IConstantsProvider constantsProvider)
        {
            _playerRepository = playerRepository;
            _solarSystemRepository = solarSystemRepository;
            _planetRepository = planetRepository;
            _entityRepository = entityRepository;
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

        public IEnumerable<SolarSystem> GenerateGalaxy()
        {
            var solarSystems = new List<SolarSystem>();
            var r = new Random();

            Parallel.For(0, Width,
                i => Parallel.For(0, Height,
                                  j =>
                                  {
                                      if (r.NextDouble() >
                                          SystemGenerationProbability)
                                      {
                                          // create a solar system here...
                                          var solarSystem = CreateSolarSystem();
                                          solarSystem.Latitude = i;
                                          solarSystem.Longitude = j;
                                          solarSystems.Add(solarSystem);
                                      }
                                  }));
            //for (var i = 0; i < Width; i += 1)
            //{
            //    for (var j = 0; j < Height; j += 1)
            //    {
            //        if (r.NextDouble() > SystemGenerationProbability)
            //        {
            //            // create a solar system here...
            //            var solarSystem = CreateSolarSystem();
            //            solarSystem.Latitude = i;
            //            solarSystem.Longitude = j;
            //            solarSystems.Add(solarSystem);
            //        }
            //    }
            //}
            foreach (var solarSystem in solarSystems)
            {
                foreach (var spatialEntity in solarSystem.SpatialEntities)
                {
                    _entityRepository.Add(spatialEntity);
                }

                //foreach (var planet in solarSystem.Planets)
                //{
                //    _planetRepository.Add(planet);
                //}

                _solarSystemRepository.Add(solarSystem);
            }
            return solarSystems;
        }

        private SolarSystem CreateSolarSystem()
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
            float max, min;

            for(var i = 0; i < numberOfEntities; i += 1)
            {
                SpatialEntityType itemType = SpatialEntityType.Planet;
                var value = r.NextDouble()*sum;
                var count = 0.0f;
                
                // Using the randomly generated value, get the item type from the array
                for(var j = 0; j < probabilityArray.Count; j += 1)
                {
                    count += probabilityArray[j].Key;
                    if (value < count)
                    {
                        itemType = probabilityArray[j].Value;
                        break;
                    }
                }

                SpatialEntity entity;

                // now create the entity!!!
                // This may be refactored to just use entity type and get rid of the Planet class.
                if(itemType == SpatialEntityType.Planet)
                {
                    entity = _planetRepository.Create();
                    solarSystem.Planets.Add((Planet)entity);
                }
                else
                {
                    entity = _entityRepository.Create();
                }
                solarSystem.SpatialEntities.Add(entity);
                
                entity.Type = itemType;

                // make the radius
                max = ssc.MaximumRadius(itemType);
                min = ssc.MinimumRadius(itemType);

                entity.Radius = r.NextDouble()*(max - min) + min;

                // make the mass
                max = ssc.MaximumMass(itemType);
                min = ssc.MinimumMass(itemType);

                // Random mass * the area of the planet (could use volume but it doesn't really matter)
                entity.Mass = r.NextDouble() * (max - min) + min;
                entity.Mass *= entity.Radius*entity.Radius*Math.PI;
            }

            // find the largest entity to use as the center of the solar system
            // TODO: to make this more awesome we might allow multiple centers of gravity
            var largestEntity = solarSystem.SpatialEntities.MaxBy(e => e.Mass);

            return solarSystem;
        }
    }
}
