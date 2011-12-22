using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space.DTO
{
    /// <summary>
    /// Bonuses used when calculating planet income
    /// </summary>
    public class PlayerBonuses
    {
        /// <summary>
        /// The player's ID
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Increase attack/defend strength
        /// </summary>
        public float MilitaryBonus { get; set; }

        /// <summary>
        /// Increase the maximum population on a planet
        /// </summary>
        public float WelfareBonus { get; set; }

        /// <summary>
        /// Income bonus
        /// </summary>
        public float EconomyBonus { get; set; }
        
        /// <summary>
        /// Construction cost bonus - cheaper buildings/units and faster building speed
        /// </summary>
        public float ConstructionBonus { get; set; }
        
        /// <summary>
        /// Resource income bonus
        /// </summary>
        public float ResourceBonus { get; set; }

        /// <summary>
        /// Reasearching bonus
        /// </summary>
        public float ResearchBonus { get; set; }
    }
}
