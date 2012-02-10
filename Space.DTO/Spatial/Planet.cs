using System;
using System.Linq;
using Space.Repository.Entities;

namespace Space.DTO.Spatial
{
    public class Planet : SpatialEntity
    {
        private const double PopulationGrowth = 5;
        private const int BasePopulation = 250;
        private const int FoodOutput = 100;
        private const int PeoplePerLivingQuarter = 650;
        private const int ResearchOutput = 20;

        public Planet()
        {
            // Initiailize all the bonuses to 1 -- 100% for calculations
            FoodBonus = 1;
            CashBonus = 1;
            IronBonus = 1;
            PlutoniumBonus = 1;

            Population = BasePopulation;
        }

        /// <summary>
        /// The owner of the planet -- Foreign Key
        /// </summary>
        public Player Owner { get; set; }

        /// <summary>
        /// The population on the planet
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// The number of buildings a planet can hold
        /// </summary>
        public int BuildingCapacity { get; set; }

        /// <summary>
        /// The buildings found on the planet
        /// </summary>
        public IQueryable<IBuilding> Buildings { get; set; }

        /// <summary>
        /// The buildings found on the planet
        /// </summary>
        public IQueryable<IUnit> Units { get; set; }

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
        public bool HasPortal { get; set; }
        public int ResearchLabCount { get; set; }

        #endregion

        public NetValue Update(PlayerBonuses bonuses)
        {
            var output = new NetValue();

            output.Cash = (100 + Population / 30 + CashFactoryCount * 8) * bonuses.EconomyBonus * CashBonus;
            output.Energy = EnergyLabCount * PlutoniumBonus;
            output.Food = FoodOutput * FarmCount * FoodBonus;
            output.Iron = MineCount * IronBonus;
            output.Population = (int)Math.Min(Population * (1 + PopulationGrowth * bonuses.WelfareBonus * 0.01f),// multiply by 0.01 to convert to percentage
                                         BasePopulation +
                                         LivingQuartersCount * PeoplePerLivingQuarter * bonuses.WelfareBonus); // cap the population
            output.Research = ResearchOutput * ResearchLabCount * bonuses.ResearchBonus;

            output.BuildingCount = CashFactoryCount + EnergyLabCount + FarmCount + LaserCount + LivingQuartersCount +
                                   ResearchLabCount;

            Population = output.Population;
            

            return output;
        }
    }
}
