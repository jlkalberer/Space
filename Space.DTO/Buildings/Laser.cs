using System;
using Space.Repository.Entities;

namespace Space.DTO.Buildings
{
    public class Laser : IBuilding
    {
        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

        #region Implementation of ISpatialEntity

        public float Latitude { get; set; }
        public float Longitude { get; set; }
       
        #endregion
    }
}
