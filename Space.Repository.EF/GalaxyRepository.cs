// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GalaxyRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the GalaxyRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Linq;

    using Space.DTO.Spatial;

    /// <summary>
    /// Defines the GalaxyRepository type.
    /// </summary>
    public class GalaxyRepository : IGalaxyRepository
    {
        /// <summary>
        /// Member for the EntityFrameworDbContext
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalaxyRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public GalaxyRepository(EntityFrameworkDbContext context)
        {
            this.context = context;
        }

        #region Implementation of ICrud<in int,Galaxy>

        /// <summary>
        /// Gets a queryable collection of entities.
        /// </summary>
        public IQueryable<Galaxy> All
        {
            get
            {
                return this.context.Galaxies;
            }
        }

        /// <summary>
        /// Gets a queryable collection of items where the returned objects are eagerly loaded.
        /// </summary>
        public IQueryable<Galaxy> EagerAll
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public Galaxy Create()
        {
            return this.context.Galaxies.Create();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public Galaxy Add(Galaxy entity)
        {
            return this.context.Galaxies.Add(entity);
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public Galaxy Get(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public bool Update(Galaxy value)
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
        
        /// <summary>
        /// Gets an eagerly loaded item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public Galaxy EagerGet(int key)
        {
            throw new NotImplementedException();
        }
    }
}