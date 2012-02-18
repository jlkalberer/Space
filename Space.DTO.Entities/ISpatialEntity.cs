// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpatialEntity.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The interface for creating any spatial entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Entities
{
    /// <summary>
    /// The interface for creating any spatial entity.
    /// </summary>
    public interface ISpatialEntity
    {
        /// <summary>
        /// Gets or sets the latitude of the entity in space.
        /// </summary>
        double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the entity in space.
        /// </summary>
        double Longitude { get; set; }
    }
}
