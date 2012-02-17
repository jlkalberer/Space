// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Galaxy.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A container for the current game session.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Spatial
{
    using System.Collections.Generic;

    using Space.DTO.Entities;
    using Space.DTO.Players;

    /// <summary>
    /// A container for the current game session.
    /// </summary>
    public class Galaxy : IDataObject
    {
        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the collection of players in the galaxy.
        /// </summary>
        public ICollection<Player> Players { get; set; } 

        /// <summary>
        /// Gets or sets the collection of solar systems in the galaxy
        /// </summary>
        public ICollection<SolarSystem> SolarSystems { get; set; }

        /// <summary>
        /// Gets or sets GalaxySettings.
        /// </summary>
        public GalaxySettings GalaxySettings { get; set; }
    }
}
