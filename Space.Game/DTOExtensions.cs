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
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The maximum buildings and the cost.
        /// </returns>
        public static NetValue MaximumBuildings(this Planet planet, Player player, BuildingType type)
        {
            int output = planet.TotalBuildings;

            var settings = player.Galaxy.GalaxySettings;
            var buildingCost = settings.BuildingCosts.SingleOrDefault(bc => bc.Type == type);

            if (buildingCost == null)
            {
                return null;
            }

            // TODO -- include empire size in calculations
            var calculationArray = new[]
                {
                    player.TotalNetValue.Cash, buildingCost.Cash, player.TotalNetValue.Energy, buildingCost.Energy,
                    player.TotalNetValue.Food, buildingCost.Food, player.TotalNetValue.Iron, buildingCost.Iron,
                    player.TotalNetValue.Mana, buildingCost.Mana
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
                for (var i = 0; i < calculationArray.Length; i += 2)
                {
                    totals[i / 2] += calculationArray[i + 1] * Math.Max(1, (output / planet.BuildingCapacity));
                    if (totals[i / 2] > calculationArray[i])
                    {
                        maxFound = true;
                    }
                }
            }
            while (!maxFound);

            // Always return 0 or greater
            return new NetValue
                {
                    BuildingCount = Math.Max(0, output - planet.TotalBuildings - 1),
                    Cash = totals[0],
                    Energy = totals[0],
                    Food = totals[0],
                    Iron = totals[0],
                    Mana = totals[0]
                };
        }

        /// <summary>
        /// Builds buildings on a planet for a player
        /// </summary>
        /// <param name="planet">
        /// The planet.
        /// </param>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <param name="costs">
        /// The costs.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// True if the buildings were successfully built.
        /// </returns>
        public static bool BuildBuildings(this Planet planet, Player player, NetValue costs, BuildingType type)
        {
            player.TotalNetValue.Subtract(costs);
            
            // Add the total building count * 2 since we just subtracted it
            player.TotalNetValue.BuildingCount += costs.BuildingCount * 2;

            // add buildings to planet
            switch (type)
            {
                case BuildingType.CashFactory:
                    planet.CashFactoryCount += costs.BuildingCount;
                    break;
                case BuildingType.EnergyLab:
                    planet.EnergyLabCount += costs.BuildingCount;
                    break;
                case BuildingType.Farm:
                    planet.FarmCount += costs.BuildingCount;
                    break;
                case BuildingType.Laser:
                    planet.LaserCount += costs.BuildingCount;
                    break;
                case BuildingType.LivingQuarters:
                    planet.LivingQuartersCount += costs.BuildingCount;
                    break;
                case BuildingType.Mana:
                    planet.ManaCount += costs.BuildingCount;
                    break;
                case BuildingType.Mine:
                    planet.MineCount += costs.BuildingCount;
                    break;
                case BuildingType.Portal:
                    planet.HasPortal = true;
                    break;
                case BuildingType.ResearchLab:
                    planet.ResearchLabCount += costs.BuildingCount;
                    break;
                case BuildingType.TaxOffice:
                    planet.TaxOfficeCount += costs.BuildingCount;
                    break;
            }

            return true;
        }
    }
}
