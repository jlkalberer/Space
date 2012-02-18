// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wizard.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A wizard unit used to cast spells.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Units
{
    using System;

    using Space.DTO.Entities;

    /// <summary>
    /// A wizard unit used to cast spells.
    /// </summary>
    public class Wizard : IUnit
    {
        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion

        #region Implementation of ISpatialEntity

        /// <summary>
        /// Gets or sets the latitude of the entity in space.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the entity in space.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets FleetID used as a primary key to the current fleet.
        /// </summary>
        public int? FleetID { get; set; }

        /// <summary>
        /// Updates the current unit.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
