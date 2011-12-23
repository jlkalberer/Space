using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Space.Repository.Entities;
using Space.DTO.Spatial;

namespace Space.DTO
{
    public class SpatialEntity : IDataObject, ISpatialEntity
    {
        public ICollection<Planet> Planets { get; set; }

        public SpatialEntityType Type { get; set; }

        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        #endregion ISpatialEntity
    }
}
