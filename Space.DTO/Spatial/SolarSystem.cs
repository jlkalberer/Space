using System.Collections.Generic;
using Space.Repository.Entities;

namespace Space.DTO
{
    public class SolarSystem : IDataObject, ISpatialEntity
    {
        public ICollection<Planet> Planets { get; set; }

        public Star Star { get; set; }

        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        #endregion ISpatialEntity
    }
}
