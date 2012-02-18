// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolarSystemConstants.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Constants for the solar systems
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    using System.Collections.Generic;

    using Space.DTO.Entities;

    /// <summary>
    /// Constants for the solar systems
    /// </summary>
    public sealed class SolarSystemConstants : IDataObject<int>
    {
        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the minimum number of entities to spawn in a solar system
        /// </summary>
        public int MinimumEntities { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of entities to spawn in a solar system
        /// </summary>
        public int MaximumEntities { get; set; }

        /// <summary>
        /// Gets or sets the collection of all spatial probabilities
        /// </summary>
        public ICollection<SpatialEntityProbabilities> SpatialEntityProbabilities { get; set; }
    }
}
