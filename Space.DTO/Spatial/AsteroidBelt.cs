// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsteroidBelt.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the AsteroidBelt type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Spatial
{
    using Space.DTO.Entities;

    /// <summary>
    /// An asteroid belt.  This will probably be added in v2.
    /// </summary>
    public class AsteroidBelt : IDataObject<int>, ISpatialEntity
    {
        #region IDataObject

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        #endregion IDataObject

        #region ISpatialEntity

        /// <summary>
        /// Gets or sets the latitude of the entity in space.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the entity in space.
        /// </summary>
        public double Longitude { get; set; }

        #endregion ISpatialEntity
    }
}
