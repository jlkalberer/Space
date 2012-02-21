// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GalaxySettingsRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The EntityFramework implementation for accessing GalaxySettings from the datastore.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using Space.DTO;

    /// <summary>
    /// The EntityFramework implementation for accessing GalaxySettings from the datastore.
    /// </summary>
    public class GalaxySettingsRepository : IGalaxySettingsRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Reference to the GalaxySettings DbQuery.
        /// </summary>
        private readonly DbQuery<GalaxySettings> settingsDbQuery; 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GalaxySettingsRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public GalaxySettingsRepository(EntityFrameworkDbContext context)
        {
            this.context = context;
            this.settingsDbQuery = this.context.GalaxySettings
                                                .Include("SolarSystemConstants")
                                                .Include("BuildingCosts")
                                                .Include("SolarSystemConstants.SpatialEntityProbabilities");
        }

        /// <summary>
        /// Gets a queryable collection of items where the returned objects are eagerly loaded.
        /// </summary>
        public IQueryable<GalaxySettings> EagerAll
        {
            get
            {
                return this.settingsDbQuery;
            }
        }

        /// <summary>
        /// Gets a queryable collection of items.
        /// </summary>
        public IQueryable<GalaxySettings> All
        {
            get
            {
                return this.context.GalaxySettings;
            }
        }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public GalaxySettings Create()
        {
            return this.context.GalaxySettings.Create();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public GalaxySettings Add(GalaxySettings entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public GalaxySettings Get(int key)
        {
            return this.context.GalaxySettings.FirstOrDefault(gs => gs.ID == key);
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public bool Update(GalaxySettings value)
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an eagerly loaded item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public GalaxySettings EagerGet(int key)
        {
            return this.settingsDbQuery.SingleOrDefault(gs => gs.ID == key);
        }
    }
}
