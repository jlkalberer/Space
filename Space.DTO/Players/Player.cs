using System;
using System.Collections.Generic;
using Space.DTO.Spatial;
using Space.Repository.Entities;

namespace Space.DTO.Players
{
    public class Player : IDataObject
    {
        /// <summary>
        /// The galaxy this player belongs to.
        /// </summary>
        public Galaxy Galaxy { get; set; }

        /// <summary>
        /// The planets the player owns
        /// </summary>
        public ICollection<Planet> Planets { get; set; }

        /// <summary>
        /// The current fleets of the player
        /// </summary>
        public ICollection<Fleet> Fleets { get; set; }

        /// <summary>
        /// The total research points currently owned by the user.  These are used to calculate the bonuses for the player.
        /// </summary>
        public ResearchPoints ResearchPoints { get; set; }

        private PlayerBonuses _bonuses;

        /// <summary>
        /// The calculated player bonuses this tick.
        /// </summary>
        public PlayerBonuses Bonuses
        {
            get
            {
                return _bonuses ?? (_bonuses = new PlayerBonuses
                                                   {
                                                       ConstructionBonus =
                                                           CalculateResearchValue(ResearchPoints.ConstructionPoints),
                                                       EconomyBonus =
                                                           CalculateResearchValue(ResearchPoints.EconomyPoints)*
                                                           Race.IncomeBonus,
                                                       MilitaryBonus =
                                                           CalculateResearchValue(ResearchPoints.MilitaryPoints)*
                                                           Race.AttackBonus,
                                                       ResearchBonus = Race.ScienceBonus,
                                                       ResourceBonus =
                                                           CalculateResearchValue(ResearchPoints.ResourcePoints),
                                                       WelfareBonus =
                                                           CalculateResearchValue(ResearchPoints.WelfarePoints)*
                                                           Race.PopulationBonus
                                                   });
            }
        }

        /// <summary>
        /// The net worth of the player.  This is the sum of all infrastructure, population, and units.
        /// </summary>
        public double NetWorth { get; set; }

        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

        /// <summary>
        /// The race of the player.  This determines their current bonuses.
        /// </summary>
        public Race Race { get; set; }

        /// <summary>
        /// The overall resources that a player has in the round.
        /// </summary>
        public NetValue TotalNetValue { get; set; }

        /// <summary>
        /// The calculated values from the last tick.
        /// </summary>
        public TickValue TickValue { get; set; }

        /// <summary>
        /// Normalized value for number of units
        /// </summary>
        public int UnitCount { get; set; }
        
        private double CalculateResearchValue(double researchPoints)
        {
            return 1+(1 - Math.Exp(-researchPoints/100*NetWorth));
        }
    }
}