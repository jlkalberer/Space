namespace Space.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Constants for the solar systems
    /// </summary>
    public sealed class SolarSystemConstants
    {
        /// <summary>
        /// The minimum number of entities to spawn in a solar system
        /// </summary>
        public int MinimumEntities { get; set; }

        /// <summary>
        /// The maximum number of entities to spawn in a solar system
        /// </summary>
        public int MaximumEntities { get; set; }

        /// <summary>
        /// The collection of all spatial probabilities
        /// </summary>
        public ICollection<SpatialEntityProbabilities> SpatialEntityProbabilities { get; set; }
    }
}
