// -----------------------------------------------------------------------
// <copyright file="ICosmicEntity.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.Repository.Entities
{
    /// <summary>
    /// Any entity that has a mass
    /// </summary>
    public interface ICosmicEntity
    {

        /// <summary>
        /// The mass of the spatial entity
        /// </summary>
        float Mass { get; set; }

        /// <summary>
        /// The radius of the entity.  Used for scaling the planet's image
        /// </summary>
        float Radius { get; set; }

        /// <summary>
        /// The distance for the obit from the most massive entity.
        /// </summary>
        float OrbitRadius { get; set; }

        /// <summary>
        /// The speed as the planet orbits around the most massive entity.
        /// We could do a fancy calculation here but it's much nicer to do it this way
        /// </summary>
        float OrbitSpeed { get; set; }
    }
}
