// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the Player type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Players
{
    using System;
    using System.Collections.Generic;

    using Space.DTO.Entities;
    using Space.DTO.Spatial;

    /// <summary>
    /// A player object for interacting in a Galaxy
    /// </summary>
    public class Player : IDataObject<int>
    {
        /// <summary>
        /// Private field for caching player bonuses.
        /// </summary>
        private PlayerBonuses bonuses;

        /// <summary>
        /// Gets or sets the galaxy this player belongs to.
        /// </summary>
        public Galaxy Galaxy { get; set; }

        /// <summary>
        /// Gets or sets the planets the player owns.
        /// </summary>
        public ICollection<Planet> Planets { get; set; }

        /// <summary>
        /// Gets or sets the current fleets of the player.
        /// </summary>
        public ICollection<Fleet> Fleets { get; set; }

        /// <summary>
        /// Gets or sets the total research points currently owned by the user.  These are used to calculate the bonuses for the player.
        /// </summary>
        public ResearchPoints ResearchPoints { get; set; }

        /// <summary>
        /// Gets the calculated player bonuses this tick.
        /// </summary>
        public PlayerBonuses Bonuses
        {
            get
            {
                return this.bonuses ?? (this.bonuses = new PlayerBonuses
                                                   {
                                                       ConstructionBonus =
                                                           this.CalculateResearchValue(this.ResearchPoints.ConstructionPoints),
                                                       EconomyBonus =
                                                           this.CalculateResearchValue(
                                                               this.ResearchPoints.EconomyPoints) * Race.IncomeBonus,
                                                       MilitaryBonus =
                                                           this.CalculateResearchValue(
                                                               this.ResearchPoints.MilitaryPoints) * Race.AttackBonus,
                                                       ResearchBonus = Race.ScienceBonus,
                                                       ResourceBonus =
                                                           this.CalculateResearchValue(this.ResearchPoints.ResourcePoints),
                                                       WelfareBonus =
                                                           this.CalculateResearchValue(
                                                               this.ResearchPoints.WelfarePoints) * Race.PopulationBonus
                                                   });
            }
        }

        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the race of the player.  This determines their current bonuses.
        /// </summary>
        public Race Race { get; set; }

        /// <summary>
        /// Gets or sets the overall resources that a player has in the round.
        /// </summary>
        public NetValue TotalNetValue { get; set; }

        /// <summary>
        /// Gets or sets the calculated values from the last tick.
        /// </summary>
        public TickValue TickValue { get; set; }

        /// <summary>
        /// Gets or sets the normalized value for number of units
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// Simple calculation to determine the research percentage for each area.
        /// </summary>
        /// <param name="researchPoints">
        /// The research points.
        /// </param>
        /// <returns>
        /// The reesarch percentage to use in calculations each tick.
        /// </returns>
        private double CalculateResearchValue(double researchPoints)
        {
            return 1 + (1 - Math.Exp(-researchPoints / 100 * this.TotalNetValue.Networth));
        }
    }
}