using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Space.Repository.Entities;

namespace Space.DTO
{
    public class Fleet : IUnit
    {
        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

        public IQueryable<IUnit> Units { get; set; }

        #region Implementation of ISpatialEntity

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int FleetID { get; set; }

        public void Update()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
