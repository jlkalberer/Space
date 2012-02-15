// -----------------------------------------------------------------------
// <copyright file="SpatialEntityProbabilities.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.DTO.Spatial;

namespace Space.DTO
{
    /// <summary>
    /// The probabilities for all spatial entities
    /// </summary>
    public class SpatialEntityProbabilities
    {
        /// <summary>
        /// The type of spatial entity assosciated with the probabilities.
        /// </summary>
        public SpatialEntityType Type { get; set; }

        /// <summary>
        /// The Spawning Probability of this type.
        /// </summary>
        public double SpawningProbability { get; set; }

        /// <summary>
        /// The Maximum Radius of this type.
        /// </summary>
        public double MaximumRadius { get; set; }

        /// <summary>
        /// The Minimum Radius of this type.
        /// </summary>
        public double MinimumRadius { get; set; }

        /// <summary>
        /// The Maximum Mass of this type.
        /// </summary>
        public double MaximumMass { get; set; }
        
        /// <summary>
        /// The Minimum Mass of this type.
        /// </summary>
        public double MinimumMass { get; set; }
    }
}
