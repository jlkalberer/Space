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
    using Space.DTO.Spatial;

    /// <summary>
    /// The Entity Framework implementation for accessing Planets from the datastore.
    /// </summary>
    public class PlanetRepository : Repository<int, Planet>, IPlanetRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public PlanetRepository(EntityFrameworkDbContext context)
            : base(context)
        {
        }
    }
}
