// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolarSystemRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The EntityFramework implementation for accessing Solar Systems from the datastore.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Linq;

    using Space.DTO.Spatial;

    /// <summary>
    /// The EntityFramework implementation for accessing Solar Systems from the datastore.
    /// </summary>
    public class SolarSystemRepository : ISolarSystemRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarSystemRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public SolarSystemRepository(EntityFrameworkDbContext context)
        {
            this.context = context;
        }

        #region Implementation of ICrud<in int,ResearchPoints>

        /// <summary>
        /// Gets a queryable collection of items.
        /// </summary>
        public IQueryable<SolarSystem> All
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public SolarSystem Create()
        {
            return this.context.SolarSystems.Create();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public SolarSystem Add(SolarSystem entity)
        {
            return this.context.SolarSystems.Add(entity);
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public SolarSystem Get(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public bool Update(SolarSystem value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an item from the datastore.
        /// </summary>
        /// <param name="key">The primary key of the item to delete.</param>
        /// <returns>The success status of the deletion.</returns>
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save the changes to the data store.
        /// </summary>
        /// <returns>The success status of the save.</returns>
        public bool SaveChanges()
        {
            this.context.SaveChanges();
            return true;
        }

        #endregion
    }
}
