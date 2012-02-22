// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetValue.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Used for calculating what a planet produces
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    /// <summary>
    /// Used for calculating what a planet produces
    /// </summary>
    public class NetValue
    {
        /// <summary>
        /// Gets or sets the foreign Key to the player
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the net worth of the player.  This is the sum of all infrastructure, population, and units.
        /// </summary>
        public double Networth { get; set; }

        /// <summary>
        /// Gets or sets Cash.
        /// </summary>
        public double Cash { get; set; }

        /// <summary>
        /// Gets or sets Energy.
        /// </summary>
        public double Energy { get; set; }

        /// <summary>
        /// Gets or sets Food.
        /// </summary>
        public double Food { get; set; }

        /// <summary>
        /// Gets or sets Population.
        /// </summary>
        public double Population { get; set; }

        /// <summary>
        /// Gets or sets Iron.
        /// </summary>
        public double Iron { get; set; }

        /// <summary>
        /// Gets or sets Mana.
        /// </summary>
        public double Mana { get; set; }

        /// <summary>
        /// Gets or sets Research.
        /// </summary>
        public double Research { get; set; }

        /// <summary>
        /// Gets or sets BuildingCount.
        /// </summary>
        public int BuildingCount { get; set; }
    }
}
