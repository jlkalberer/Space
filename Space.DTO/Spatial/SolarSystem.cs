using System.Collections.Generic;
using System.Collections.ObjectModel;
using Space.Repository.Entities;

namespace Space.DTO.Spatial
{
    public class SolarSystem : IDataObject, ISpatialEntity
    {
        public SolarSystem()
        {
            SpatialEntities = new Collection<SpatialEntity>();
            Planets = new Collection<Planet>();
        }

        /// <summary>
        /// The ID of the parent galaxy
        /// </summary>
        public int GalaxyID { get; set; }

        /// <summary>
        /// The planets in the system
        /// </summary>
        public ICollection<Planet> Planets { get; set; }

        /// <summary>
        /// All spatial entities in the system
        /// </summary>
        public ICollection<SpatialEntity> SpatialEntities { get; set; }

        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        #endregion ISpatialEntity
    }
}
