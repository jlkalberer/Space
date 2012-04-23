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
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using Space.DTO;

    /// <summary>
    /// The EntityFramework implementation for accessing GalaxySettings from the datastore.
    /// </summary>
    public class GalaxySettingsRepository : Repository<int, GalaxySettings>, IGalaxySettingsRepository
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
            : base(context)
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
