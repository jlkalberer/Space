// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LivingQuarters.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the LivingQuarters type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Buildings
{
    using Space.DTO.Entities;

    /// <summary>
    ///   Defines the LivingQuarters type.
    /// </summary>
    public class LivingQuarters : IBuilding
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
       
        #endregion
    }
}
