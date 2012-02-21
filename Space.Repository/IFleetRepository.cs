// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFleetRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The repository interface used for Fleets.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO;

    /// <summary>
    /// The repository interface used for Fleets.
    /// </summary>
    public interface IFleetRepository : ICrud<int, Fleet>
    {
    }
}
