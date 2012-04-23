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
    using Space.DTO.Spatial;

    /// <summary>
    /// The EntityFramework implementation for accessing Solar Systems from the datastore.
    /// </summary>
    public class SolarSystemRepository : Repository<int, SolarSystem>, ISolarSystemRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolarSystemRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public SolarSystemRepository(EntityFrameworkDbContext context)
            : base(context)
        {
        }
    }
}
