// -----------------------------------------------------------------------
// <copyright file="Galaxy.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.Repository.Entities;

namespace Space.DTO.Spatial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
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
        /// The collection of solar systems in the galaxy
        /// </summary>
        public ICollection<SolarSystem> SolarSystems { get; set; }

        /// <summary>
        /// Settings used to generate the galaxy.  These are used for creation and rendering.
        /// </summary>
        public GalaxySettings GalaxySettings { get; set; }
    }
}
