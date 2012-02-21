// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolarSystemConstants.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A solar system which is part of a galaxy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Spatial
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Space.DTO.Entities;

    /// <summary>
    /// A solar system which is part of a galaxy.
    /// </summary>
    public class SolarSystem : IDataObject<int>, ISpatialEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolarSystem"/> class.
        /// </summary>
        public SolarSystem()
        {
            this.SpatialEntities = new Collection<SpatialEntity>();
            this.Planets = new Collection<Planet>();
        }

        /// <summary>
        /// Gets or sets the ID of the parent galaxy
        /// </summary>
        public int GalaxyID { get; set; }

        /// <summary>
        /// Gets or sets the planets in the system
        /// </summary>
        public ICollection<Planet> Planets { get; set; }

        /// <summary>
        /// Gets or sets all spatial entities in the system
        /// </summary>
        public ICollection<SpatialEntity> SpatialEntities { get; set; }

        #region IDataObject

        /// <summary>
        /// Gets or sets the primary key for the entity
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
