// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fleet.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A fleet object used to house all units which are a part of it.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    using System;
    using System.Linq;

    using Space.DTO.Entities;

    /// <summary>
    /// A fleet object used to house all units which are a part of it.
    /// </summary>
    public class Fleet : IUnit
    {
        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets Units which are part of the fleet.
        /// </summary>
        public IQueryable<IUnit> Units { get; set; }

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
