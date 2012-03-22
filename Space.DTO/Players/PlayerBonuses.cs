// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerBonuses.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Bonuses used when calculating planet income
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Players
{
    /// <summary>
    /// Bonuses used when calculating planet income
    /// </summary>
    public class PlayerBonuses
    {
        /// <summary>
        /// Gets or sets the player's ID
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the increase attack/defend strength
        /// </summary>
        public double MilitaryBonus { get; set; }

        /// <summary>
        /// Gets or sets the increase the maximum population on a planet
        /// </summary>
        public double WelfareBonus { get; set; }

        /// <summary>
        /// Gets or sets the income bonus
        /// </summary>
        public double EconomyBonus { get; set; }
        
        /// <summary>
        /// Gets or sets the construction cost bonus - cheaper buildings/units and faster building speed
        /// </summary>
        public double ConstructionBonus { get; set; }
        
        /// <summary>
        /// Gets or sets the resource income bonus
        /// </summary>
        public double ResourceBonus { get; set; }

        /// <summary>
        /// Gets or sets the reasearching bonus
        /// </summary>
        public double ResearchBonus { get; set; }
    }
}
