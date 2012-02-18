// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpatialEntity.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Any type of cosmic entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Spatial
{
    using Space.DTO.Entities;

    /// <summary>
    /// Any type of cosmic entity.
    /// </summary>
    public class SpatialEntity : IDataObject<int>, ISpatialEntity, ICosmicEntity
    {
        /// <summary>
        /// Gets or sets the type of entity is this.  Used for rendering and physics.
        /// </summary>
        public SpatialEntityType Type { get; set; }

        /// <summary>
        /// Gets or sets the solar system this planet belongs to
        /// </summary>
        public SolarSystem SolarSystem { get; set; }

        #region IDataObject

        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        /// <summary>
        /// Gets or sets the latitude of the entity in space.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the entity in space.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the entity in space.
        /// </summary>
        public double Longsitude { get; set; }

        #endregion ISpatialEntity

        #region Implementation of ICosmicEntity

        /// <summary>
        /// Gets or sets the mass of the spatial entity
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Gets or sets the radius of the entity.  Used for scaling the planet's image
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets the distance for the obit from the most massive entity.
        /// </summary>
        public double OrbitRadius { get; set; }

        /// <summary>
        /// Gets or sets the speed as the planet orbits around the most massive entity.
        /// We could do a fancy calculation here but it's much nicer/easier to do it this way
        /// </summary>
        public double OrbitSpeed { get; set; }

        #endregion
    }
}
