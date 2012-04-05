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
    using System.Collections.Generic;

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
            var min1 = planet.Population * (1 + (settings.PopulationGrowth * bonuses.WelfareBonus * 0.01));
                
            var min2 = settings.BasePopulation + (settings.MaxPopulationPerBuildings * planet.BuildingCapacity)
                       + (planet.LivingQuartersCount * settings.PeoplePerLivingQuarter * bonuses.WelfareBonus);
            
            // cap the population
            output.Population = Math.Min(min1, min2);
                
            planet.PopulationGrowth = output.Population - planet.Population;
            planet.Population = output.Population;

            // cash calculations
            var cashBonus = bonuses.EconomyBonus * planet.CashBonus;

            // so the user always makes cash
            var positiveIncome = settings.PositiveIncomeCash * cashBonus;
            var buildingCount = Math.Max(output.BuildingCount, 1);
            output.CashFactoryCash = planet.CashFactoryCount * settings.CashOutput * cashBonus;
            output.PopulationCash = planet.Population / settings.PopulationCashDivider * cashBonus;
            output.TaxOfficeCash = (double)planet.TaxOfficeCount / buildingCount
                                   * (positiveIncome + output.CashFactoryCash + output.PopulationCash);

            output.Cash = positiveIncome + output.CashFactoryCash + output.PopulationCash + output.TaxOfficeCash;
            
            output.Energy = planet.EnergyLabCount * planet.EnergyBonus;
            output.Food = settings.FoodOutput * planet.FarmCount * planet.FoodBonus;
            output.Iron = planet.MineCount * planet.IronBonus;
            output.Mana = planet.ManaCount * planet.ManaBonus;

            output.Research = settings.ResearchOutput * planet.ResearchLabCount * planet.ResearchBonus * bonuses.ResearchBonus;

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
            tickValue.DecayedMana = player.TotalNetValue.Mana * decay;

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
            player.TotalNetValue.Mana -= tickValue.DecayedMana;

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
            netValue.Mana += value.Mana;
            netValue.Population += value.Population;
            netValue.Iron += value.Iron;
            netValue.Research += value.Research;
            netValue.BuildingCount += value.BuildingCount;
        }

        /// <summary>
        /// Subtract all resources from one another.
        /// </summary>
        /// <param name="netValue">
        /// The net value.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void Subtract(this NetValue netValue, NetValue value)
        {
            netValue.Cash -= value.Cash;
            netValue.Energy -= value.Energy;
            netValue.Food -= value.Food;
            netValue.Mana -= value.Mana;
            netValue.Population -= value.Population;
            netValue.Iron -= value.Iron;
            netValue.Research -= value.Research;
            netValue.BuildingCount -= value.BuildingCount;
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
        /// <typeparam name="TType">
        /// The type of build cost.
        /// </typeparam>
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="buildingCost">
        /// The building Cost.
        /// </param>
        /// <returns>
        /// The maximum buildings and the cost.
        /// </returns>
        public static NetValue MaximumToBeBuilt<TType>(this Planet planet, Player player, BuildCosts<TType> buildingCost)
        {
            int output = planet.TotalBuildings;

            var totalNetValue = player.TotalNetValue;

            // TODO -- include empire size in calculations
            var calculationArray = new[]
                {
                    totalNetValue.Cash, buildingCost.Cash, totalNetValue.Energy, buildingCost.Energy,
                    totalNetValue.Food, buildingCost.Food, totalNetValue.Iron, buildingCost.Iron,
                    totalNetValue.Mana, buildingCost.Mana
                };

            var maxCounts = new List<int>();

            for (var i = 0; i < calculationArray.Length; i += 2)
            {
                maxCounts.Add((int)Math.Floor(calculationArray[i] / calculationArray[i + 1]));
            }

            var maxFound = false;
            var totals = new double[calculationArray.Length / 2];
            do
            {
                output++;

                // This will tell us to kick out of the loop if the totals never get incremented.
                var totalNotEqualToZero = 0;
                for (var i = 0; i < calculationArray.Length; i += 2)
                {
                    totals[i / 2] += calculationArray[i + 1] * Math.Max(1, (output / planet.BuildingCapacity));
                    if (totals[i / 2] > 0)
                    {
                        totalNotEqualToZero += 1;
                    }

                    if (totals[i / 2] <= calculationArray[i])
                    {
                        continue;
                    }

                    maxFound = true;
                    break;
                }

                if (totalNotEqualToZero == 0)
                {
                    maxFound = true;
                }
            }
            while (!maxFound);

            return new NetValue
                {
                    // Always return 0 or greater
                    BuildingCount = Math.Max(0, output - planet.TotalBuildings - 1),
                    Cash = totals[0],
                    Energy = totals[1],
                    Food = totals[2],
                    Iron = totals[3],
                    Mana = totals[4]
                };
        }

        /// <summary>
        /// Builds buildings on a planet for a player
        /// </summary>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="costs">
        /// The costs.
        /// </param>
        /// <returns>
        /// True if the buildings were successfully built.
        /// </returns>
        public static bool BuildBuildings(this Player player, NetValue costs)
        {
            player.TotalNetValue.Subtract(costs);

            // NOTE - add the buildings back to the player since the Subtract function deletes them
            player.TotalNetValue.BuildingCount += costs.BuildingCount;

            return true;
        }

        /// <summary>
        /// Adds buildings to a planet.
        /// </summary>
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="player">
        /// The player to add the buildings to.
        /// </param>
        /// <param name="buildingCount">
        /// The building count.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// True if building succeeds.
        /// </returns>
        public static bool AddBuildings(this Planet planet, Player player, int buildingCount, BuildingType type)
        {
            player.TotalNetValue.BuildingCount += buildingCount;

            // add buildings to planet
            switch (type)
            {
                case BuildingType.CashFactory:
                    planet.CashFactoryCount += buildingCount;
                    break;
                case BuildingType.EnergyLab:
                    planet.EnergyLabCount += buildingCount;
                    break;
                case BuildingType.Farm:
                    planet.FarmCount += buildingCount;
                    break;
                case BuildingType.Laser:
                    planet.LaserCount += buildingCount;
                    break;
                case BuildingType.LivingQuarters:
                    planet.LivingQuartersCount += buildingCount;
                    break;
                case BuildingType.Mana:
                    planet.ManaCount += buildingCount;
                    break;
                case BuildingType.Mine:
                    planet.MineCount += buildingCount;
                    break;
                case BuildingType.Portal:
                    planet.HasPortal = true;
                    break;
                case BuildingType.ResearchLab:
                    planet.ResearchLabCount += buildingCount;
                    break;
                case BuildingType.TaxOffice:
                    planet.TaxOfficeCount += buildingCount;
                    break;
            }

            return true;
        }
    }
}
