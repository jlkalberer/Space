using Space.DTO.Entities;

namespace Space.DTO.Spatial
{
    /// <summary>
    /// Any type of cosmic entity.
    /// </summary>
    public class SpatialEntity : IDataObject, ISpatialEntity, ICosmicEntity
    {
        /// <summary>
        /// What type of entity is this.  Used for rendering and physics.
        /// </summary>
        public SpatialEntityType Type { get; set; }

        /// <summary>
        /// What solar system this planet belongs to
        /// </summary>
        public SolarSystem SolarSystem { get; set; }

        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion ISpatialEntity

        #region Implementation of ICosmicEntity

        /// <summary>
        /// The mass of the spatial entity
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// The radius of the entity.  Used for scaling the planet's image
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The distance for the obit from the most massive entity.
        /// </summary>
        public double OrbitRadius { get; set; }

        /// <summary>
        /// The speed as the planet orbits around the most massive entity.
        /// We could do a fancy calculation here but it's much nicer to do it this way
        /// </summary>
        public double OrbitSpeed { get; set; }

        #endregion
    }
}
