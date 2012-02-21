// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityFrameworkInitializer.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Initializes the database with default values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Spatial;
    using Space.Infrastructure;

    /// <summary>
    /// Initializes the database with default values.
    /// </summary>
    public class EntityFrameworkInitializer : DropCreateDatabaseAlways<EntityFrameworkDbContext>
    {
        #region Implementation of IDatabaseInitializer<in EntityFrameworkDbContext>

        /// <summary>
        /// Executes the strategy to initialize the database for the given context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(EntityFrameworkDbContext context)
        {
            base.Seed(context);
            if (!context.Database.Exists() || !context.Database.CompatibleWithModel(true))
            {
                return;
            }

            var galaxySettings = new GalaxySettings();
            galaxySettings.Width = 10;
            galaxySettings.Height = 10;
            galaxySettings.SystemGenerationProbability = 0.94;
            galaxySettings.OrbitSpeedMaximum = 100000.0;
            galaxySettings.OrbitSpeedMaximum = 30000.0;
            galaxySettings.SolarSystemScalar = 1;
            galaxySettings.Decay = 0.005;

            galaxySettings.PopulationGrowth = 5;
            galaxySettings.MaxPopulationPerBuildings = 40;
            galaxySettings.BasePopulation = 250;
            galaxySettings.CashOutput = 8;
            galaxySettings.FoodOutput = 100;
            galaxySettings.PeoplePerLivingQuarter = 650;
            galaxySettings.ResearchOutput = 20;

            var solarSystemConstants = new SolarSystemConstants();
            solarSystemConstants.MaximumEntities = 25;
            solarSystemConstants.MinimumEntities = 10;

            var planetProbabilities = new List<SpatialEntityProbabilities>();

            foreach (var type in SystemTypes.EnumToList<SpatialEntityType>())
            {
                planetProbabilities.Add(new SpatialEntityProbabilities
                    {
                        Type = type,
                        MaximumMass = 1,
                        MaximumRadius = 1,
                        MinimumMass = 1,
                        MinimumRadius = 1,
                        SpawningProbability = .5
                    });
            }

            var star = planetProbabilities.First(o => o.Type == SpatialEntityType.Star);
            star.MaximumMass = star.MinimumMass = 2;
            var planet = planetProbabilities.First(o => o.Type == SpatialEntityType.Planet);
            planet.SpawningProbability = 5;

            solarSystemConstants.SpatialEntityProbabilities = planetProbabilities;
            galaxySettings.SolarSystemConstants = solarSystemConstants;

            var buildingCosts = new List<BuildingCosts>
                {
                    new BuildingCosts
                        {
                            Type = BuildingType.CashFactory,
                            Cash = 120,
                            Energy = 1,
                            Food = 0,
                            Iron = 10,
                            Mana = 0,
                            Time = 5
                        },
                    new BuildingCosts
                        {
                            Type = BuildingType.EnergyLab,
                            Cash = 160,
                            Energy = 1,
                            Food = 0,
                            Iron = 3,
                            Mana = 0,
                            Time = 12
                        },
                   new BuildingCosts
                        {
                            Type = BuildingType.Farm,
                            Cash = 300,
                            Energy = 0,
                            Food = 0,
                            Iron = 20,
                            Mana = 0,
                            Time = 10
                        },
                new BuildingCosts
                        {
                            Type = BuildingType.Laser,
                            Cash = 700,
                            Energy = 1,
                            Food = 0,
                            Iron = 35,
                            Mana = 0,
                            Time = 8
                        },
                   new BuildingCosts
                        {
                            Type = BuildingType.LivingQuarters,
                            Cash = 200,
                            Energy = 1,
                            Food = 0,
                            Iron = 25,
                            Mana = 0,
                            Time = 8
                        },
                   new BuildingCosts
                        {
                            Type = BuildingType.Mine,
                            Cash = 200,
                            Energy = 1,
                            Food = 5,
                            Iron = 0,
                            Mana = 0,
                            Time = 12
                        },
                   new BuildingCosts
                        {
                            Type = BuildingType.Portal,
                            Cash = 15000,
                            Energy = 110,
                            Food = 0,
                            Iron = 400,
                            Mana = 80,
                            Time = 40
                        },
                   new BuildingCosts
                        {
                            Type = BuildingType.ResearchLab,
                            Cash = 100,
                            Energy = 1,
                            Food = 0,
                            Iron = 0,
                            Mana = 0,
                            Time = 14
                        },
                   new BuildingCosts
                        {
                            Type = BuildingType.TaxOffice,
                            Cash = 200,
                            Energy = 1,
                            Food = 0,
                            Iron = 15,
                            Mana = 0,
                            Time = 16
                        },
                };

            galaxySettings.BuildingCosts = buildingCosts;

            context.GalaxySettings.Add(galaxySettings);

            context.SaveChanges();
        }

        #endregion
    }
}
