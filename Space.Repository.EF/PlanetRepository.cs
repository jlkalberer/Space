// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlanetRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The Entity Framework implementation for accessing Planets from the datastore.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Linq;

    using Space.DTO.Spatial;

    /// <summary>
    /// The Entity Framework implementation for accessing Planets from the datastore.
    /// </summary>
    public class PlanetRepository : IPlanetRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public PlanetRepository(EntityFrameworkDbContext context)
        {
            this.context = context;
        }

        #region Implementation of ICrud<in int,Planet>

        /// <summary>
        /// Gets a queryable collection of entities.
        /// </summary>
        public IQueryable<Planet> All
        {
            get
            {
                return this.context.SpatialEntities.OfType<Planet>();
            }
        }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public Planet Create()
        {
            return this.context.SpatialEntities.Create<Planet>();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public Planet Add(Planet entity)
        {
            return this.context.SpatialEntities.Add(entity) as Planet;
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public Planet Get(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public bool Update(Planet value)
        {
            this.context.Entry(value);
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
