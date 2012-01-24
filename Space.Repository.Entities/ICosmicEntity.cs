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
    }
}
