using Space.Repository.Entities;

namespace Space.DTO.Spatial
{
    public class SpatialEntity : IDataObject, ISpatialEntity, ICosmicEntity
    {
        /// <summary>
        /// What type of entity is this.  Used for rendering and physics.
        /// </summary>
        public SpatialEntityType Type { get; set; }

        /// <summary>
        /// What solar system this planet belongs to
        /// </summary>
        public int SolarSystemID { get; set; }

        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        #endregion ISpatialEntity

        #region Implementation of ICosmicEntity

        /// <summary>
        /// The mass of the spatial entity
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// The radius of the entity.  Used for scaling the planet's image
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// The distance for the obit from the most massive entity.
        /// </summary>
        public float OrbitRadius { get; set; }

        /// <summary>
        /// The speed as the planet orbits around the most massive entity.
        /// We could do a fancy calculation here but it's much nicer to do it this way
        /// </summary>
        public float OrbitSpeed { get; set; }

        #endregion
    }
}
