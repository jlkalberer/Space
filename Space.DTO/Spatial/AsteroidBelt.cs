using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Space.Repository.Entities;

namespace Space.DTO.Spatial
{
    public class AsteroidBelt : IDataObject, ISpatialEntity
    {
        #region IDataObject

        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        #endregion ISpatialEntity
    }
}
