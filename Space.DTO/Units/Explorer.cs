using System;
using Space.Repository.Entities;

namespace Space.DTO.Units
{
    public class Explorer : IUnit
    {
        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

        #region Implementation of ISpatialEntity

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int FleetID { get; set; }
        public void Update()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
