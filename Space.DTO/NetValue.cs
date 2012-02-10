namespace Space.DTO
{
    /// <summary>
    /// Used for calculating what a planet produces
    /// </summary>
    public class NetValue
    {
        /// <summary>
        /// Add all resources together
        /// </summary>
        /// <param name="value"></param>
        public void Add(NetValue value)
        {
            Cash += value.Cash;
            Energy += value.Energy;
            Food += value.Food;
            Population += value.Population;
            Iron += value.Iron;
            Research += value.Research;
            BuildingCount += value.BuildingCount;
        }

        /// <summary>
        /// Foreign Key to the player
        /// </summary>
        public int PlayerID { get; set; }

        public double Cash { get; set; }
        public double Energy { get; set; }
        public double Food { get; set; }
        public int Population { get; set; }
        public double Iron { get; set; }
        public double Research { get; set; }

        public int BuildingCount { get; set; }
    }
}
