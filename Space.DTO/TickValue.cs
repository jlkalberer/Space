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
        /// Gets or sets the foreign Key to the player
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets if the population starving last tick.  Did the player run out of food?
        /// </summary>
        public bool IsPopulationStarving { get; set; }

        /// <summary>
        /// Gets or sets how much Cash was lost during the tick.
        /// </summary>
        public double DecayedCash { get; set; }
        
        /// <summary>
        /// Gets or sets how much Energy was lost during the tick.
        /// </summary>
        public double DecayedEnergy { get; set; }

        /// <summary>
        /// Gets or sets how much Food was lost during the tick.
        /// </summary>
        public double DecayedFood { get; set; }

        /// <summary>
        /// Gets or sets how much Iron was lost during the tick.
        /// </summary>
        public double DecayedIron { get; set; }

        /// <summary>
        /// Gets or sets how much Mana was lost during the tick.
        /// </summary>
        public double DecayedMana { get; set; }

        /// <summary>
        /// Gets or sets how much cash was produced during the tick.
        /// </summary>
        public double ProducedCashFactoryCash { get; set; }

        /// <summary>
        /// Gets or sets how much cash was produced during the tick.
        /// </summary>
        public double ProducedPopulationCash { get; set; }

        /// <summary>
        /// Gets or sets how much cash was produced during the tick.
        /// </summary>
        public double ProducedTaxOfficeCash { get; set; }

        /// <summary>
        /// Gets or sets how much cash was produced during the tick.
        /// </summary>
        public double ProducedCash { get; set; }
        
        /// <summary>
        /// Gets or sets how much cash was produced during the tick.
        /// </summary>
        public double NetCash { get; set; }

        /// <summary>
        /// Gets or sets how much energy was produced during the tick.
        /// </summary>
        public double ProducedEnergy { get; set; }

        /// <summary>
        /// Gets or sets how much food was produced during the tick.
        /// </summary>
        public double ProducedFood { get; set; }

        /// <summary>
        /// Gets or sets how much the population increased during the tick.
        /// </summary>
        public double ProducedPopulation { get; set; }

        /// <summary>
        /// Gets or sets how much iron was produced during the tick.
        /// </summary>
        public double ProducedIron { get; set; }

        /// <summary>
        /// Gets or sets how much research was produced during the tick.
        /// </summary>
        public double ProducedResearch { get; set; }

        /// <summary>
        /// Gets or sets how many buildings were maintained during the tick.
        /// </summary>
        public double Buildings { get; set; }

        /// <summary>
        /// Gets or sets how many units were maintained during the tick.
        /// </summary>
        public double Units { get; set; }
    }
}
