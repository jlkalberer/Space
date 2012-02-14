namespace Space.DTO.Players
{
    /// <summary>
    /// The total points allocated to each research area
    /// </summary>
    public class ResearchPoints
    {
        public int PlayerID { get; set; }

        /// <summary>
        /// The number of Military Points that have been allocated from research.  This boosts attacking stats.
        /// </summary>
        public double MilitaryPoints { get; set; }

        /// <summary>
        /// The number of Welfare Points that have been allocated from research.  This helps population growth.
        /// </summary>
        public double WelfarePoints { get; set; }

        /// <summary>
        /// The number of Economy Points that have been allocated from research.  This boosts the amount of cash generated each tick.
        /// </summary>
        public double EconomyPoints { get; set; }

        /// <summary>
        /// The number of Construction Points that have been allocated from research.  This lowers construction costs and time to build.
        /// </summary>
        public double ConstructionPoints { get; set; }

        /// <summary>
        /// The number of Resource Points that have been allocated from research.  This boosts the amount of resources generated each tick.
        /// </summary>
        public double ResourcePoints { get; set; }
    }
}
