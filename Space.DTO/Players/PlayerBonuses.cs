namespace Space.DTO.Players
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
        public double MilitaryBonus { get; set; }

        /// <summary>
        /// Increase the maximum population on a planet
        /// </summary>
        public double WelfareBonus { get; set; }

        /// <summary>
        /// Income bonus
        /// </summary>
        public double EconomyBonus { get; set; }
        
        /// <summary>
        /// Construction cost bonus - cheaper buildings/units and faster building speed
        /// </summary>
        public double ConstructionBonus { get; set; }
        
        /// <summary>
        /// Resource income bonus
        /// </summary>
        public double ResourceBonus { get; set; }

        /// <summary>
        /// Reasearching bonus
        /// </summary>
        public double ResearchBonus { get; set; }
    }
}
