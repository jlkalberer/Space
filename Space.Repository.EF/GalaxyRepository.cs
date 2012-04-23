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
    public class GalaxyRepository : Repository<int, Galaxy>, IGalaxyRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GalaxyRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public GalaxyRepository(EntityFrameworkDbContext context)
            : base(context)
        {
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