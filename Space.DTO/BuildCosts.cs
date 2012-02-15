namespace Space.DTO
{
    using Buildings;

    /// <summary>
    /// Keeps track of building costs for members.
    /// </summary>
    public sealed class BuildCosts
    {
        /// <summary>
        /// The type of building assosciated with the costs.
        /// </summary>
        public BuildingType Type { get; set; }

        /// <summary>
        /// Access the building cost in Cash
        /// </summary>
        public double Cash { get; set; }

        /// <summary>
        /// Access the building cost in Food
        /// </summary>
        public double Food { get; set; }

        /// <summary>
        /// Access the building cost in Iron
        /// </summary>
        public double Iron { get; set; }

        /// <summary>
        /// Access the building cost in Energy
        /// </summary>
        public double Energy { get; set; }

        /// <summary>
        /// Access the building cost in Mana
        /// </summary>
        public double Mana { get; set; }
    }
}
