// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the Game type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MoreLinq;

    using Space.DTO;
    using Space.DTO.Spatial;
    using Space.Repository;

    /// <summary>
    /// Used to run an instance of the game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Constant variable used for calculations of circles.
        /// </summary>
        public const double CircleCoefficient = 2 * Math.PI;

        /// <summary>
        /// Repository to access any players.
        /// </summary>
        private readonly IPlayerRepository playerRepository;

        /// <summary>
        /// Repository to access any galaxies.
        /// </summary>
        private readonly IGalaxyRepository galaxyRepository;

        /// <summary>
        /// Repository to access any galaxy settings.
        /// </summary>
        private readonly IGalaxySettingsRepository galaxySettingsRepository;

        /// <summary>
        /// Repository to access any solar systems.
        /// </summary>
        private readonly ISolarSystemRepository solarSystemRepository;

        /// <summary>
        /// Repository to access any planets.
        /// </summary>
        private readonly IPlanetRepository planetRepository;

        /// <summary>
        /// Repository to access any spatial entities.
        /// </summary>
        private readonly ISpatialEntityRepository spatialEntityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="playerRepository">
        /// The player repository.
        /// </param>
        /// <param name="galaxyRepository">
        /// The galaxy repository.
        /// </param>
        /// <param name="galaxySettingsRepository">
        /// The Galaxy Settings repository.
        /// </param>
        /// <param name="solarSystemRepository">
        /// The solar system repository.
        /// </param>
        /// <param name="planetRepository">
        /// The planet repository.
        /// </param>
        /// <param name="spatialEntityRepository">
        /// The entity repository.
        /// </param>
        public Game(
            IPlayerRepository playerRepository,
            IGalaxyRepository galaxyRepository,
            IGalaxySettingsRepository galaxySettingsRepository,
            ISolarSystemRepository solarSystemRepository,
            IPlanetRepository planetRepository,
            ISpatialEntityRepository spatialEntityRepository)
        {
            this.playerRepository = playerRepository;
            this.galaxyRepository = galaxyRepository;
            this.galaxySettingsRepository = galaxySettingsRepository;
            this.solarSystemRepository = solarSystemRepository;
            this.planetRepository = planetRepository;
            this.spatialEntityRepository = spatialEntityRepository;
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
            var planetsToUpdate = this.planetRepository.All.Where(p => p.Owner != null).GroupBy(p => p.Owner.ID);

            planetsToUpdate
                .AsParallel()
                .ForAll(planetSet =>
                            {
                                var user = this.playerRepository.Get(planetSet.Key);
                                if (user == null)
                                {
                                    return;
                                }

                                var netTotalValue = new PlanetValue();
                                var galaxySettings = user.Galaxy.GalaxySettings;

                                foreach (var planet in planetSet)
                                {
                                    var netValue = planet.Update(galaxySettings, user.Bonuses);
                                    netTotalValue.Add(netValue);
                                    this.planetRepository.Update(planet);
                                }

                                user.Update(netTotalValue, galaxySettings);
                            });

            this.playerRepository.SaveChanges();
        }

        /// <summary>
        /// Creates a Galaxy using supplied settings.
        /// </summary>
        /// <param name="galaxyID">
        /// The galaxy to use as a template
        /// </param>
        /// <returns>
        /// A new Galaxy stored in a datastore.
        /// </returns>
        public Galaxy GenerateGalaxy(int? galaxyID)
        {
            var solarSystems = new List<SolarSystem>();
            var r = new Random();

            GalaxySettings settings;
            if (!galaxyID.HasValue)
            {
                settings = this.galaxySettingsRepository.EagerAll.First();
            }
            else
            {
                settings = this.galaxySettingsRepository.EagerGet(galaxyID.GetValueOrDefault());
            }

            Parallel.For(
                0,
                settings.Width,
                i => Parallel.For(
                    0,
                    settings.Height,
                    j =>
                    {
                        if (r.NextDouble() <=
                            settings.SystemGenerationProbability)
                        {
                            return;
                        }

                        // create a solar system here...
                        var solarSystem = CreateSolarSystem(settings);

                        if (solarSystem == null)
                        {
                            return;
                        }

                        solarSystem.Latitude = i;
                        solarSystem.Longitude = j;
                        solarSystems.Add(solarSystem);
                    }));

            foreach (var solarSystem in solarSystems)
            {
                foreach (var spatialEntity in solarSystem.SpatialEntities)
                {
                    this.spatialEntityRepository.Add(spatialEntity);
                }

                this.solarSystemRepository.Add(solarSystem);
            }

            var output = new Galaxy
                             {
                                 GalaxySettings = settings,
                                 SolarSystems = solarSystems
                             };

            output = this.galaxyRepository.Add(output);

            return output;
        }

        /// <summary>
        /// Creates a SolarSystemConstants in a Galaxy.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <returns>
        /// A new SolarSystemConstants stored in a datastore.
        /// </returns>
        private SolarSystem CreateSolarSystem(GalaxySettings settings)
        {
            var solarSystem = this.solarSystemRepository.Create();

            // get the type of solar system -- what type of central mass it has
            var r = new Random();
            var ssc = settings.SolarSystemConstants;

            var numberOfEntities = r.Next(ssc.MinimumEntities, ssc.MaximumEntities);

            // Create this array so we can use it to decide which entity is to be created
            var probabilityArray = (from e in Enum.GetValues(typeof(SpatialEntityType)).Cast<SpatialEntityType>()
                                    from o in ssc.SpatialEntityProbabilities
                                    where o.Type == e
                                    select new KeyValuePair<double, SpatialEntityType>(
                                        o.SpawningProbability, e)).ToList();

            // Get the sum of the probability array
            var sum = probabilityArray.Select(o => o.Key).Sum();

            for (var i = 0; i < numberOfEntities; i += 1)
            {
                SpatialEntityType itemType = SpatialEntityType.Planet;
                var value = r.NextDouble() * sum;
                var count = 0.0;

                // Using the randomly generated value, get the item type from the array
                for (var j = 0; j < probabilityArray.Count; j += 1)
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
                if (itemType == SpatialEntityType.Planet)
                {
                    entity = this.planetRepository.Create();
                    solarSystem.Planets.Add((Planet)entity);
                    var planet = entity as Planet;

                    // TODO: figure out the planet building capacity and bonuses here....
                    if (planet != null)
                    {
                        planet.BuildingCapacity = r.Next(150, 350);
                    }
                }
                else
                {
                    entity = this.spatialEntityRepository.Create();
                }

                if (entity == null)
                {
                    continue;
                }

                solarSystem.SpatialEntities.Add(entity);

                entity.Type = itemType;

                var entitySettings = ssc.SpatialEntityProbabilities.FirstOrDefault(o => o.Type == itemType);

                if (entitySettings == null)
                {
                    throw new Exception("foobar");
                }

                // make the radius
                var max = entitySettings.MaximumRadius;
                var min = entitySettings.MinimumRadius;

                entity.Radius = (r.NextDouble() * (max - min)) + min;

                // make the mass
                max = entitySettings.MaximumMass;
                min = entitySettings.MinimumMass;

                // Random mass * the area of the planet (could use volume but it doesn't really matter)
                entity.Mass = (r.NextDouble() * (max - min)) + min;
                entity.Mass *= entity.Radius * entity.Radius * Math.PI;
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

                entity.OrbitRadius = Math.Pow(16 * i, 2.2) * settings.SolarSystemScalar;

                var randomRadians = r.NextDouble() * CircleCoefficient;
                entity.Latitude = entity.OrbitRadius * Math.Cos(randomRadians);
                entity.Longitude = entity.OrbitRadius * Math.Sin(randomRadians);

                entity.OrbitSpeed = ((r.NextDouble() * settings.OrbitSpeedDifference) + settings.OrbitSpeedMinimum) * settings.SolarSystemScalar;
            }

            solarSystem.SpatialEntities = entities;

            return solarSystem;
        }
    }
}
