// -----------------------------------------------------------------------
// <copyright file="GalaxySettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Space.Infrastructure;
    using Space.DTO.Buildings;
    using Space.Repository;
    using Space.Repository.Entities;

    /// <summary>
    /// Game setup used to create a galaxy
    /// </summary>
    public class GalaxySettings
    {
        /// <summary>
        /// Access constants stored in a datastore.
        /// </summary>
        private readonly IConstantsProvider _constantsProvider;

        public GalaxySettings(IConstantsProvider constantsProvider)
        {
            _constantsProvider = constantsProvider;

            var keyPart = "GalaxySettings.BuildCosts.";
            BuildingCosts = new Dictionary<BuildingType, BuildCosts>();
            foreach (var buildType in SystemTypes.EnumToList<BuildingType>())
            {
                BuildingCosts.Add(buildType, new BuildCosts(keyPart + buildType + ".", _constantsProvider));
            }
            SolarSystem = new SolarSystemConstants("GalaxySettings.SolarSystemConstants,", _constantsProvider);
        }

        #region Galaxy Setup
        /// <summary>
        /// The ID of the Galaxy the settings belong to.
        /// </summary>
        public int GalaxyID { get; set; }

        /// <summary>
        /// The width of the solar system
        /// </summary>
        public int Width = 10;
        
        /// <summary>
        /// The height of the solar system.
        /// </summary>
        public int Height = 10;
        
        /// <summary>
        /// The probability of how many systems will be generated.
        /// </summary>
        public double SystemGenerationProbability = 0.94f;
        
        /// <summary>
        /// The maximum speed a planet can move while orbiting a mass.
        /// </summary>
        public double OrbitSpeedMaximum = 100000.0f;
        
        /// <summary>
        /// The minimum speed a planet can move while orbiting a mass.
        /// </summary>
        public double OrbitSpeedMinimum = 30000.0f;

        /// <summary>
        /// This is used to scale how far apart the planets are relative to the solar system.
        /// </summary>
        public double SolarSystemScalar = 1;

        /// <summary>
        /// The decay of resources each tick in the galaxy
        /// </summary>
        public double Decay = 0.005f;

        /// <summary>
        /// Used multiple times in calculations to get the difference between the orbit speed maximum and minimum.
        /// </summary>
        public double OrbitSpeedDifference { get { return OrbitSpeedMaximum - OrbitSpeedMinimum; } }
        #endregion

        #region Planet Defaults

        /// <summary>
        /// The rate at which populations grow.
        /// </summary>
        public double PopulationGrowth = 5;

        /// <summary>
        /// Used to calculate the base population of a planet.  If the Maximum number of buildings (before overbuilding) is 150, 
        /// the maximum population is 40 * 150.
        /// </summary>
        public double MaxPopulationPerBuildings = 40;

        /// <summary>
        /// This is the minimum number of inhabitants on a planet.
        /// </summary>
        public int BasePopulation = 250;

        /// <summary>
        /// How much cash is output from the cash factory each tick.
        /// </summary>
        public double CashOutput = 8;

        /// <summary>
        /// How much food is output from each farm each tick.
        /// </summary>
        public double FoodOutput = 100;

        /// <summary>
        /// The maximum number of people that can live in the living quarters.
        /// </summary>
        public double PeoplePerLivingQuarter = 650;

        /// <summary>
        /// How much research is generated from the research stations.
        /// </summary>
        public double ResearchOutput = 20;

        #endregion

        public SolarSystemConstants SolarSystem { get; set; }

        /// <summary>
        /// Dictionary of build costs for all building types.
        /// </summary>
        public Dictionary<BuildingType, BuildCosts> BuildingCosts { get; set; }
    }
}
