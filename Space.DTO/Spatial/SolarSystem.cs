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

        public ICollection<Planet> Planets { get; set; }

        public ICollection<SpatialEntity> SpatialEntities { get; set; }

        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion ISpatialEntity
    }
}
