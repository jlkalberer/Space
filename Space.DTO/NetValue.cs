namespace Space.DTO
{
    /// <summary>
    /// Used for calculating what a planet produces
    /// </summary>
    public class NetValue
    {
        /// <summary>
        /// Foreign Key to the player
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// The net worth of the player.  This is the sum of all infrastructure, population, and units.
        /// </summary>
        public double Networth { get; set; }
        public double Cash { get; set; }
        public double Energy { get; set; }
        public double Food { get; set; }
        public double Population { get; set; }
        public double Iron { get; set; }
        public double Research { get; set; }

        public int BuildingCount { get; set; }
    }
}
