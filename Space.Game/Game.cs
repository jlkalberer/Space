using System;
using System.Linq;
using System.Reflection;
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
        public const double CircleCoefficient = 2 * Math.PI;

        private readonly IPlayerRepository _playerRepository;
        private readonly IGalaxyRepository _galaxyRepository;
        private readonly ISolarSystemRepository _solarSystemRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IConstantsProvider _constantsProvider;

        public Game(IPlayerRepository playerRepository, IGalaxyRepository galaxyRepository, ISolarSystemRepository solarSystemRepository,
            IPlanetRepository planetRepository, IEntityRepository entityRepository, IConstantsProvider constantsProvider)
        {
            _playerRepository = playerRepository;
            _galaxyRepository = galaxyRepository;
            _solarSystemRepository = solarSystemRepository;
            _planetRepository = planetRepository;
            _entityRepository = entityRepository;
            _constantsProvider = constantsProvider;

            var type = typeof (Planet);
            var v=type.GetField("PopulationGrowth");
            foreach (var m in type.GetFields(BindingFlags.NonPublic | BindingFlags.Static))
            {
                m.SetValue(null, 10);
            }
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

                                var galaxySettings = user.Galaxy.GalaxySettings;

                                foreach (var planet in planetSet)
                                {
                                    var netValue = planet.Update(galaxySettings, user.Bonuses);
                                    netTotalValue.Add(netValue);
                                    _planetRepository.Update(planet);
                                }

                                user.Update(netTotalValue);
                            });

            _playerRepository.SaveChanges();
        }

        public Galaxy GenerateGalaxy(GalaxySettings settings)
        {
            var solarSystems = new List<SolarSystem>();
            var r = new Random();

            Parallel.For(0, settings.Width,
                i => Parallel.For(0, settings.Height,
                                  j =>
                                  {
                                      if (r.NextDouble() >
                                          settings.SystemGenerationProbability)
                                      {
                                          // create a solar system here...
                                          var solarSystem = CreateSolarSystem(settings);
                                          solarSystem.Latitude = i;
                                          solarSystem.Longitude = j;
                                          solarSystems.Add(solarSystem);
                                      }
                                  }));

            foreach (var solarSystem in solarSystems)
            {
                foreach (var spatialEntity in solarSystem.SpatialEntities)
                {
                    _entityRepository.Add(spatialEntity);
                }

                _solarSystemRepository.Add(solarSystem);
            }

            var output = new Galaxy
                             {
                                 GalaxySettings = settings,
                                 SolarSystems = solarSystems
                             };

            output = _galaxyRepository.Add(output);

            return output;
        }

        private SolarSystem CreateSolarSystem(GalaxySettings settings)
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
                    var planet = entity as Planet;

                    // TODO: figure out the planet building capacity and bonuses here....
                    planet.BuildingCapacity = r.Next(150, 350);
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

            solarSystem.SpatialEntities.Remove(largestEntity);
            solarSystem.SpatialEntities.Add(largestEntity);
            var entities = solarSystem.SpatialEntities.Reverse().ToList();

            // give all the spatial entities properties for placement and movement
            for (var i = 1; i < entities.Count; i++)
            {
                var entity = entities[i];

                entity.OrbitRadius = (float)Math.Pow(16*i, 2.2) * settings.SolarSystemScalar;
                
                var randomRadians = r.NextDouble()*CircleCoefficient;
                entity.Latitude = (float)(entity.OrbitRadius * Math.Cos(randomRadians));
                entity.Longitude = (float)(entity.OrbitRadius * Math.Sin(randomRadians));

                entity.OrbitSpeed = (float)(r.NextDouble() * settings.OrbitSpeedDifference + settings.OrbitSpeedMinimum) * settings.SolarSystemScalar;
            }

            solarSystem.SpatialEntities = entities;

            return solarSystem;
        }
    }
}
