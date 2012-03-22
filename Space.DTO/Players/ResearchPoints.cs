// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResearchPoints.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The total points allocated to each research area
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Players
{
    /// <summary>
    /// The total points allocated to each research area
    /// </summary>
    public class ResearchPoints
    {
        /// <summary>
        /// Gets or sets PlayerID.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the number of Military Points that have been allocated from research.  This boosts attacking stats.
        /// </summary>
        public double MilitaryPoints { get; set; }

        /// <summary>
        /// Gets or sets the number of Welfare Points that have been allocated from research.  This helps population growth.
        /// </summary>
        public double WelfarePoints { get; set; }

        /// <summary>
        /// Gets or sets the number of Economy Points that have been allocated from research.  This boosts the amount of cash generated each tick.
        /// </summary>
        public double EconomyPoints { get; set; }

        /// <summary>
        /// Gets or sets the number of Construction Points that have been allocated from research.  This lowers construction costs and time to build.
        /// </summary>
        public double ConstructionPoints { get; set; }

        /// <summary>
        /// Gets or sets the number of Resource Points that have been allocated from research.  This boosts the amount of resources generated each tick.
        /// </summary>
        public double ResourcePoints { get; set; }
    }
}
