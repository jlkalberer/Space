// -----------------------------------------------------------------------
// <copyright file="TickValue.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.DTO
{
    /// <summary>
    /// Stored after every tick
    /// </summary>
    public class TickValue
    {
        /// <summary>
        /// Foreign Key to the player
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Was the population starving last tick?  Did the player run out of food?
        /// </summary>
        public bool IsPopulationStarving { get; set; }

        /// <summary>
        /// How much Cash was lost during the tick.
        /// </summary>
        public double DecayedCash { get; set; }
        
        /// <summary>
        /// How much Energy was lost during the tick.
        /// </summary>
        public double DecayedEnergy { get; set; }

        /// <summary>
        /// How much Food was lost during the tick.
        /// </summary>
        public double DecayedFood { get; set; }

        /// <summary>
        /// How much Iron was lost during the tick.
        /// </summary>
        public double DecayedIron { get; set; }

        /// <summary>
        /// How much cash was produced during the tick.
        /// </summary>
        public double ProducedCashFactoryCash { get; set; }

        /// <summary>
        /// How much cash was produced during the tick.
        /// </summary>
        public double ProducedPopulationCash { get; set; }

        /// <summary>
        /// How much cash was produced during the tick.
        /// </summary>
        public double ProducedTaxOfficeCash { get; set; }

        /// <summary>
        /// How much cash was produced during the tick.
        /// </summary>
        public double ProducedCash { get; set; }
        
        /// <summary>
        /// How much cash was produced during the tick.
        /// </summary>
        public double NetCash { get; set; }

        /// <summary>
        /// How much energy was produced during the tick.
        /// </summary>
        public double ProducedEnergy { get; set; }

        /// <summary>
        /// How much food was produced during the tick.
        /// </summary>
        public double ProducedFood { get; set; }

        /// <summary>
        /// How much the population increased during the tick.
        /// </summary>
        public double ProducedPopulation { get; set; }

        /// <summary>
        /// How much iron was produced during the tick.
        /// </summary>
        public double ProducedIron { get; set; }

        /// <summary>
        /// How much research was produced during the tick.
        /// </summary>
        public double ProducedResearch { get; set; }

        /// <summary>
        /// How many buildings were maintained during the tick.
        /// </summary>
        public double Buildings { get; set; }

        /// <summary>
        /// How many units were maintained during the tick.
        /// </summary>
        public double Units { get; set; }
    }
}
