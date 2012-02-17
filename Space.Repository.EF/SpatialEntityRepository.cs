// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpatialEntityRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The EntityFramework implementation for accessing Spatial Entities from the datastore.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Data;
    using System.Linq;

    using Space.DTO.Spatial;

    /// <summary>
    /// The EntityFramework implementation for accessing Spatial Entities from the datastore.
    /// </summary>
    public class SpatialEntityRepository : ISpatialEntityRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpatialEntityRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public SpatialEntityRepository(EntityFrameworkDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets a queryable collection of items.
        /// </summary>
        public IQueryable<SpatialEntity> All
        {
            get { return this.context.SpatialEntities.OfType<SpatialEntity>(); }
        }

        #region Implementation of ICrud<in int,Planet>

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public SpatialEntity Create()
        {
            return this.context.SpatialEntities.Create<SpatialEntity>();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public SpatialEntity Add(SpatialEntity entity)
        {
            return this.context.SpatialEntities.Add(entity);
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public SpatialEntity Get(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public bool Update(SpatialEntity value)
        {
            var entry = this.context.Entry(value);
            entry.State = EntityState.Modified;
            return true;
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
