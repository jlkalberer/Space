﻿using System.Linq;
using Space.DTO.Entities;
using Space.DTO.Players;

namespace Space.DTO.Spatial
{
    public class Planet : SpatialEntity
    {
        public Planet()
        {
            // Initiailize all the bonuses to 1 -- 100% for calculations
            FoodBonus = 1;
            CashBonus = 1;
            IronBonus = 1;
            PlutoniumBonus = 1;
        }

        /// <summary>
        /// The owner of the planet -- Foreign Key
        /// </summary>
        public Player Owner { get; set; }

        /// <summary>
        /// The population on the planet
        /// </summary>
        public double Population { get; set; }

        /// <summary>
        /// The growth in population between the last tick and this tick.
        /// </summary>
        public double PopulationGrowth { get; set; }

        /// <summary>
        /// The number of buildings a planet can hold
        /// </summary>
        public int BuildingCapacity { get; set; }

        // probably don't need this...
        ///// <summary>
        ///// The buildings found on the planet
        ///// </summary>
        //public IQueryable<IBuilding> Buildings { get; set; }

        /// <summary>
        /// The buildings found on the planet
        /// </summary>
        public IQueryable<IUnit> Units { get; set; }

        /// <summary>
        /// The planet's number in order of distance from the system's gravitational mass.
        /// </summary>
        public int PlanetNumber { get; set; }

        #region Bonuses
        /// <summary>
        /// The bonus of Food when producing Food
        /// </summary>
        public double FoodBonus { get; set; }

        /// <summary>
        /// The bonus of Cash when producing Cash
        /// </summary>
        public double CashBonus { get; set; }

        /// <summary>
        /// The bonus of Iron when producing Iron
        /// </summary>
        public double IronBonus { get; set; }

        /// <summary>
        /// The bonus of Plutonium when producing Plutonium
        /// </summary>
        public double PlutoniumBonus { get; set; }

        #endregion

        #region Normalized Building Count

        public int CashFactoryCount { get; set; }
        public int EnergyLabCount { get; set; }
        public int FarmCount { get; set; }
        public int LaserCount { get; set; }
        public int LivingQuartersCount { get; set; }
        public int MineCount { get; set; }
        public int TaxOfficeCount { get; set; }
        public bool HasPortal { get; set; }
        public int ResearchLabCount { get; set; }

        #endregion
    }
}
