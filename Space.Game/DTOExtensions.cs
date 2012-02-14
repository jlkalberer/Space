// -----------------------------------------------------------------------
// <copyright file="DTOExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.DTO;
using Space.DTO.Players;
using Space.DTO.Spatial;

namespace Space.Game
{
    using System;

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
        /// <returns></returns>
        public static PlanetValue Update(this Planet planet, GalaxySettings settings, PlayerBonuses bonuses)
        {
            var output = new PlanetValue();
            
            output.BuildingCount = planet.CashFactoryCount + planet.EnergyLabCount + planet.FarmCount + planet.LaserCount + planet.LivingQuartersCount +
                                   planet.ResearchLabCount;

            // Set the base population
            if (Math.Abs(planet.Population - 0) < settings.BasePopulation)
            {
                planet.Population = settings.BasePopulation;
            }
            output.Population = Math.Min(planet.Population * (1 + settings.PopulationGrowth * bonuses.WelfareBonus * 0.01f),// multiply by 0.01 to convert to percentage
                                         settings.BasePopulation + settings.MaxPopulationPerBuildings * planet.BuildingCapacity +
                                         planet.LivingQuartersCount * settings.PeoplePerLivingQuarter * bonuses.WelfareBonus); // cap the population

            planet.PopulationGrowth = output.Population - planet.Population;
            planet.Population = output.Population;

            // cash calculations
            var cashBonus = bonuses.EconomyBonus*planet.CashBonus;
            var positiveIncome = 100 * cashBonus;//so the user always makes cash
            output.CashFactoryCash = planet.CashFactoryCount * settings.CashOutput * cashBonus;
            output.PopulationCash = planet.Population/30*cashBonus;
            output.TaxOfficeCash = (double)planet.TaxOfficeCount / (output.BuildingCount + 1) * (positiveIncome + output.CashFactoryCash + output.PopulationCash);
            
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

            #region TickValues...

            // Decay
            var decay = settings.Decay;
            tickValue.DecayedCash = player.TotalNetValue.Cash * decay;
            tickValue.DecayedEnergy = player.TotalNetValue.Energy * decay;
            tickValue.DecayedFood = player.TotalNetValue.Food * decay;
            tickValue.DecayedIron = player.TotalNetValue.Iron * decay;

            // Produced
            tickValue.ProducedBuildings = totalPlanetValue.BuildingCount;
            tickValue.ProducedCashFactoryCash = totalPlanetValue.CashFactoryCash;
            tickValue.ProducedPopulationCash = totalPlanetValue.PopulationCash;
            tickValue.ProducedTaxOfficeCash = totalPlanetValue.TaxOfficeCash;
            tickValue.ProducedCash = totalPlanetValue.Cash;
            tickValue.ProducedEnergy = totalPlanetValue.Energy;
            tickValue.ProducedFood = totalPlanetValue.Food;
            tickValue.ProducedIron = totalPlanetValue.Iron;
            tickValue.ProducedPopulation = totalPlanetValue.Population;
            tickValue.ProducedResearch = totalPlanetValue.Research;
            
            #endregion

            // Decay existing values from last tick
            player.TotalNetValue.Cash -= tickValue.DecayedCash;
            player.TotalNetValue.Energy -= tickValue.DecayedEnergy;
            player.TotalNetValue.Food -= tickValue.DecayedFood;
            player.TotalNetValue.Iron -= tickValue.DecayedIron;

            // Add new values
            player.TotalNetValue.Add(totalPlanetValue);

            player.TotalNetValue.Population = totalPlanetValue.Population;

            // let them eat cake! -- but not so much cake that it goes below zero...
            player.TotalNetValue.Food -= player.TotalNetValue.Population / 10.0 + player.UnitCount;

            if (player.TotalNetValue.Food < 0)
            {
                player.TotalNetValue.Food = 0;
                player.TotalNetValue.Cash -= totalPlanetValue.Cash / 2;
                tickValue.IsPopulationStarving = true;
            }

            // building maintainance & unit upkeep
            player.TotalNetValue.Cash = Math.Max(0, player.TotalNetValue.Cash - player.TotalNetValue.BuildingCount + player.UnitCount);

            // TODO - allocate the research points...
        }

        /// <summary>
        /// Add all resources together
        /// </summary>
        /// <param name="netValue"> </param>
        /// <param name="value"></param>
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
        /// Add all resources together
        /// </summary>
        /// <param name="planet"> </param>
        /// <param name="value"></param>
        public static void Add(this PlanetValue planet, PlanetValue value)
        {
            ((NetValue)planet).Add(value as NetValue);
            planet.CashFactoryCash += value.CashFactoryCash;
            planet.PopulationCash += value.PopulationCash;
            planet.TaxOfficeCash += value.TaxOfficeCash;
        }
    }
}
