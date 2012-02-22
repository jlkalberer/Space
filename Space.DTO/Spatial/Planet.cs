// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Planet.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A planet spatial entity.  This is a container used to house buildings and accrue income and resources.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Spatial
{
    using System.Linq;

    using Space.DTO.Entities;
    using Space.DTO.Players;

    /// <summary>
    /// A planet spatial entity.  This is a container used to house buildings and accrue income and resources.
    /// </summary>
    public class Planet : SpatialEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Planet"/> class.
        /// </summary>
        public Planet()
        {
            // Initiailize all the bonuses to 1 -- 100% for calculations
            this.FoodBonus = 1;
            this.CashBonus = 1;
            this.IronBonus = 1;
            this.PlutoniumBonus = 1;
        }

        /// <summary>
        /// Gets or sets the owner of the planet.
        /// </summary>
        public Player Owner { get; set; }

        /// <summary>
        /// Gets or sets the population on the planet.
        /// </summary>
        public double Population { get; set; }

        /// <summary>
        /// Gets or sets the growth in population between the last tick and this tick.
        /// </summary>
        public double PopulationGrowth { get; set; }

        /// <summary>
        /// Gets or sets the number of buildings a planet can hold
        /// </summary>
        public int BuildingCapacity { get; set; }

        /// <summary>
        /// Gets or sets the buildings found on the planet
        /// </summary>
        public IQueryable<IUnit> Units { get; set; }

        /// <summary>
        /// Gets or sets the planet's number in order of distance from the system's gravitational mass.
        /// </summary>
        public int PlanetNumber { get; set; }

        #region Bonuses
        /// <summary>
        /// Gets or sets the bonus of Food when producing Food
        /// </summary>
        public double FoodBonus { get; set; }

        /// <summary>
        /// Gets or sets the bonus of Cash when producing Cash
        /// </summary>
        public double CashBonus { get; set; }

        /// <summary>
        /// Gets or sets the bonus of Iron when producing Iron
        /// </summary>
        public double IronBonus { get; set; }

        /// <summary>
        /// Gets or sets the bonus of Plutonium when producing Plutonium
        /// </summary>
        public double PlutoniumBonus { get; set; }

        #endregion

        #region Normalized Building Count

        /// <summary>
        /// Gets or sets CashFactoryCount.
        /// </summary>
        public int CashFactoryCount { get; set; }

        /// <summary>
        /// Gets or sets EnergyLabCount.
        /// </summary>
        public int EnergyLabCount { get; set; }

        /// <summary>
        /// Gets or sets FarmCount.
        /// </summary>
        public int FarmCount { get; set; }

        /// <summary>
        /// Gets or sets LaserCount.
        /// </summary>
        public int LaserCount { get; set; }

        /// <summary>
        /// Gets or sets LivingQuartersCount.
        /// </summary>
        public int LivingQuartersCount { get; set; }

        /// <summary>
        /// Gets or sets MineCount.
        /// </summary>
        public int MineCount { get; set; }

        /// <summary>
        /// Gets or sets TaxOfficeCount.
        /// </summary>
        public int TaxOfficeCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HasPortal.
        /// </summary>
        public bool HasPortal { get; set; }

        /// <summary>
        /// Gets or sets ResearchLabCount.
        /// </summary>
        public int ResearchLabCount { get; set; }

        /// <summary>
        /// Gets the total number of buildings on a planet.  A portal does not count in this calculation.
        /// </summary>
        public int TotalBuildings
        {
            get
            {
                return this.CashFactoryCount + this.EnergyLabCount + this.FarmCount + this.LaserCount + this.LivingQuartersCount + this.MineCount
                       + this.TaxOfficeCount + this.ResearchLabCount;
            }
        }

        #endregion
    }
}
