// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GalaxySettings.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Game setup used to create a galaxy
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    using System.Collections.Generic;

    using Space.DTO.Entities;

    /// <summary>
    /// Game setup used to create a galaxy
    /// </summary>
    public class GalaxySettings : IDataObject<int>
    {
        #region Galaxy Setup
        
        /// <summary>
        /// Gets or sets the GalaxySettings primary key.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the Galaxy the settings belong to.
        /// </summary>
        public int GalaxyID { get; set; }

        /// <summary>
        /// Gets or sets the width of the solar system
        /// </summary>
        public int Width { get; set; }
        
        /// <summary>
        /// Gets or sets the height of the solar system.
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// Gets or sets the probability of how many systems will be generated.
        /// </summary>
        public double SystemGenerationProbability { get; set; }
        
        /// <summary>
        /// Gets or sets the maximum speed a planet can move while orbiting a mass.
        /// </summary>
        public double OrbitSpeedMaximum { get; set; }
        
        /// <summary>
        /// Gets or sets the minimum speed a planet can move while orbiting a mass.
        /// </summary>
        public double OrbitSpeedMinimum { get; set; }

        /// <summary>
        /// Gets or sets the solar system scalar.
        /// This is used to scale how far apart the planets are relative to the solar system.
        /// </summary>
        public double SolarSystemScalar { get; set; }

        /// <summary>
        /// Gets or sets the decay of resources each tick in the galaxy
        /// </summary>
        public double Decay { get; set; }

        /// <summary>
        /// Gets the difference between the maximum and minimum orbital speeds.
        /// </summary>
        public double OrbitSpeedDifference
        {
            get
            {
                return this.OrbitSpeedMaximum - this.OrbitSpeedMinimum;
            }
        }

        #endregion

        #region Planet Defaults

        /// <summary>
        /// Gets or sets the rate at which populations grow.
        /// </summary>
        public double PopulationGrowth { get; set; }

        /// <summary>
        /// Gets or sets a value used to calculate the maximum population of a planet.  If the Maximum number of buildings (before overbuilding) is 150, 
        /// the maximum population is 40 * 150.
        /// </summary>
        public double MaxPopulationPerBuildings { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of inhabitants on a planet.
        /// </summary>
        public int BasePopulation { get; set; }

        /// <summary>
        /// Gets or sets how much cash is output from the cash factory each tick.
        /// </summary>
        public double CashOutput { get; set; }

        /// <summary>
        /// Gets or sets how much food is output from each farm each tick.
        /// </summary>
        public double FoodOutput { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of people that can live in the living quarters.
        /// </summary>
        public double PeoplePerLivingQuarter { get; set; }

        /// <summary>
        /// Gets or sets How much research is generated from the research stations.
        /// </summary>
        public double ResearchOutput { get; set; }

        #endregion

        public NetValue PlayerStartingValues { get; set; }

        /// <summary>
        /// Gets or sets SolarSystemConstants.
        /// </summary>
        public SolarSystemConstants SolarSystemConstants { get; set; }

        /// <summary>
        /// Gets or sets the collection of build costs for all building types.
        /// </summary>
        public ICollection<BuildingCosts> BuildingCosts { get; set; }
    }
}
