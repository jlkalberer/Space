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
    using Space.DTO.Spatial;

    /// <summary>
    /// The EntityFramework implementation for accessing Spatial Entities from the datastore.
    /// </summary>
    public class SpatialEntityRepository : Repository<int, SpatialEntity>, ISpatialEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpatialEntityRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public SpatialEntityRepository(EntityFrameworkDbContext context)
            : base(context)
        {
        }
    }
}
