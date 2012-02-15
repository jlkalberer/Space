﻿using System;
using Space.DTO.Entities;

namespace Space.DTO.Units
{
    public class Bomber : IUnit
    {
        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

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
