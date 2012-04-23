// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The EntityFramework implementation for accessing Players from the datastore.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using Space.DTO.Players;

    /// <summary>
    /// The EntityFramework implementation for accessing Players from the datastore.
    /// </summary>
    public class PlayerRepository : Repository<int, Player>, IPlayerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public PlayerRepository(EntityFrameworkDbContext context) 
            : base(context)
        {
        }
    }
}
