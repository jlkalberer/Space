// -----------------------------------------------------------------------
// <copyright file="Galaxy.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.DTO.Entities;
using Space.DTO.Players;

namespace Space.DTO.Spatial
{
    using System.Collections.Generic;

    /// <summary>
    /// A container for the current game session.
    /// </summary>
    public class Galaxy : IDataObject
    {
        #region Implementation of IDataObject

        /// <summary>
        /// Used as the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// The collection of players in the galaxy.
        /// </summary>
        public ICollection<Player> Players { get; set; } 

        /// <summary>
        /// The collection of solar systems in the galaxy
        /// </summary>
        public ICollection<SolarSystem> SolarSystems { get; set; }
    }
}
