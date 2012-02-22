// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DTOExtensions.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Extension methods for DTOs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Game
{
    using System;
    using System.Linq;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// Extension methods for DTOs
    /// </summary>
    public static class DTOExtensions
    {
        /// <summary>
        /// Update a planet and determine population for tick.
        /// </summary>
        /// <param name="planet">The planet to update.</param>
        /// <param name="settings">Settings for the current Galaxy.</param>
        /// <param name="bonuses">Bonuses for the player based on their race.</param>
        /// <returns>The calculated values from the planet.</returns>
        public static PlanetValue Update(this Planet planet, GalaxySettings settings, PlayerBonuses bonuses)
        {
            var output = new PlanetValue();
            
            output.BuildingCount = planet.TotalBuildings;

            // Set the base population
            if (Math.Abs(planet.Population - 0) < settings.BasePopulation)
            {
                planet.Population = settings.BasePopulation;
            }

            // multiply by 0.01 to convert to percentage
            var min1 = planet.Population * (1 + (settings.PopulationGrowth * bonuses.WelfareBonus * 0.01f));
                
            var min2 = settings.BasePopulation + (settings.MaxPopulationPerBuildings * planet.BuildingCapacity)
                       + (planet.LivingQuartersCount * settings.PeoplePerLivingQuarter * bonuses.WelfareBonus);
            
            // cap the population
            output.Population = Math.Min(min1, min2);
                
            planet.PopulationGrowth = output.Population - planet.Population;
            planet.Population = output.Population;

            // cash calculations
            var cashBonus = bonuses.EconomyBonus * planet.CashBonus;

            // so the user always makes cash
            var positiveIncome = 100 * cashBonus;
            output.CashFactoryCash = planet.CashFactoryCount * settings.CashOutput * cashBonus;
            output.PopulationCash = planet.Population / 30 * cashBonus;
            output.TaxOfficeCash = (double)planet.TaxOfficeCount / (output.BuildingCount + 1)
                                   * (positiveIncome + output.CashFactoryCash + output.PopulationCash);

            output.Cash = positiveIncome + output.CashFactoryCash + output.PopulationCash + output.TaxOfficeCash;
            
            output.Energy = planet.EnergyLabCount * planet.PlutoniumBonus;
            output.Food = settings.FoodOutput * planet.FarmCount * planet.FoodBonus;
            output.Iron = planet.MineCount * planet.IronBonus;

            output.Research = settings.ResearchOutput * planet.ResearchLabCount * bonuses.ResearchBonus;

            return output;
        }

        /// <summary>
        /// Updates the player's resources each tick - This uses the calculations generated from all planets.
        /// </summary>
        /// <param name="player">The player to update.</param>
        /// <param name="totalPlanetValue">The calculated values from all planets.</param>
        /// <param name="settings">Settings used for this galaxy.</param>
        public static void Update(this Player player, PlanetValue totalPlanetValue, GalaxySettings settings)
        {
            var tickValue = player.TickValue;

            // Decay
            var decay = settings.Decay;
            tickValue.DecayedCash = player.TotalNetValue.Cash * decay;
            tickValue.DecayedEnergy = player.TotalNetValue.Energy * decay;
            tickValue.DecayedFood = player.TotalNetValue.Food * decay;
            tickValue.DecayedIron = player.TotalNetValue.Iron * decay;

            // Produced
            tickValue.Buildings = totalPlanetValue.BuildingCount;
            tickValue.Units = player.UnitCount;
            tickValue.ProducedCashFactoryCash = totalPlanetValue.CashFactoryCash;
            tickValue.ProducedPopulationCash = totalPlanetValue.PopulationCash;
            tickValue.ProducedTaxOfficeCash = totalPlanetValue.TaxOfficeCash;
            tickValue.ProducedCash = totalPlanetValue.Cash;
            tickValue.ProducedEnergy = totalPlanetValue.Energy;
            tickValue.ProducedFood = totalPlanetValue.Food;
            tickValue.ProducedIron = totalPlanetValue.Iron;
            tickValue.ProducedPopulation = totalPlanetValue.Population;
            tickValue.ProducedResearch = totalPlanetValue.Research;

            // Decay existing values from last tick
            player.TotalNetValue.Cash -= tickValue.DecayedCash;
            player.TotalNetValue.Energy -= tickValue.DecayedEnergy;
            player.TotalNetValue.Food -= tickValue.DecayedFood;
            player.TotalNetValue.Iron -= tickValue.DecayedIron;

            // Add new values
            player.TotalNetValue.Add(totalPlanetValue);

            player.TotalNetValue.Population = totalPlanetValue.Population;

            // let them eat cake! -- but not so much cake that it goes below zero...
            player.TotalNetValue.Food -= (player.TotalNetValue.Population / 10.0) + player.UnitCount;

            if (player.TotalNetValue.Food < 0)
            {
                player.TotalNetValue.Food = 0;
                player.TotalNetValue.Cash -= totalPlanetValue.Cash / 2;
                tickValue.IsPopulationStarving = true;
            }

            // building maintainance & unit upkeep
            player.TotalNetValue.Cash = Math.Max(0, player.TotalNetValue.Cash - (player.TotalNetValue.BuildingCount + player.UnitCount));
            
            // TODO - allocate the research points...
        }

        /// <summary>
        /// Add all resources together.
        /// </summary>
        /// <param name="netValue">
        /// The net value.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void Add(this NetValue netValue, NetValue value)
        {
            netValue.Cash += value.Cash;
            netValue.Energy += value.Energy;
            netValue.Food += value.Food;
            netValue.Population += value.Population;
            netValue.Iron += value.Iron;
            netValue.Research += value.Research;
            netValue.BuildingCount += value.BuildingCount;
        }

        /// <summary>
        /// Add all resources together.
        /// </summary>
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void Add(this PlanetValue planet, PlanetValue value)
        {
            planet.Add(value as NetValue);
            planet.CashFactoryCash += value.CashFactoryCash;
            planet.PopulationCash += value.PopulationCash;
            planet.TaxOfficeCash += value.TaxOfficeCash;
        }

        /// <summary>
        /// Calculates the maximum number of buildings a player can build on a particular planet.
        /// </summary>
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The maximum buildings.
        /// </returns>
        public static int MaximumBuildings(this Planet planet, Player player, BuildingType type)
        {
            int output = 0;

            var settings = player.Galaxy.GalaxySettings;
            var buildingCost = settings.BuildingCosts.SingleOrDefault(bc => bc.Type == type);

            if (buildingCost == null)
            {
                return output;
            }

            // TODO -- include empire size in calculations
            var calculationArray = new[]
                {
                    player.TotalNetValue.Cash, buildingCost.Cash, player.TotalNetValue.Energy, buildingCost.Energy,
                    player.TotalNetValue.Food, buildingCost.Food, player.TotalNetValue.Iron, buildingCost.Iron,
                    player.TotalNetValue.Mana, buildingCost.Mana
                };

            for (var i = 0; i < calculationArray.Length; i += 2)
            {
                if (calculationArray[i] < 0.1)
                {
                    continue;
                }

                var maxCount = (int)Math.Floor(calculationArray[i] / calculationArray[i + 1]);
                output = Math.Max(output, Maximum(calculationArray[i], calculationArray[i + 1], maxCount, planet.BuildingCapacity));
            }

            return output;
        }

        /// <summary>
        /// Gets the maximum number that can be created with when taking 
        /// </summary>
        /// <param name="assetValue">
        /// The asset value.
        /// </param>
        /// <param name="cost">
        /// The cost.
        /// </param>
        /// <param name="maxCount">
        /// The max count.
        /// </param>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        /// <returns>
        /// The maximum value.
        /// </returns>
        private static int Maximum(double assetValue, double cost, int maxCount, int capacity = -1)
        {
            if (cost > 0.1)
            {
                return int.MaxValue;
            }

            var minCount = 0;
            int mid;

            do
            {
                mid = (minCount + maxCount) / 2;
                if (assetValue > cost * mid * Math.Max(1, (mid / capacity) - 1))
                {
                    minCount = mid + 1;
                }
                else
                {
                    maxCount = mid - 1;
                }
            }
            while (minCount < maxCount);

            return mid;
        }
    }
}
