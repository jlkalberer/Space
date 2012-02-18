// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICosmicEntity.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Any entity that has a mass
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Entities
{
    /// <summary>
    /// Any entity that has a mass
    /// </summary>
    public interface ICosmicEntity
    {
        /// <summary>
        /// Gets or sets the mass of the spatial entity
        /// </summary>
        double Mass { get; set; }

        /// <summary>
        /// Gets or sets the radius of the entity.  Used for scaling the planet's image
        /// </summary>
        double Radius { get; set; }

        /// <summary>
        /// Gets or sets the distance for the obit from the most massive entity.
        /// </summary>
        double OrbitRadius { get; set; }

        /// <summary>
        /// Gets or sets the speed as the planet orbits around the most massive entity.
        /// We could do a fancy calculation here but it's much nicer/easier to do it this way
        /// </summary>
        double OrbitSpeed { get; set; }
    }
}
