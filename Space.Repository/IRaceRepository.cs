// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRaceRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the IRaceRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO.Players;

    /// <summary>
    ///   Defines the IRaceRepository type.
    /// </summary>
    public interface IRaceRepository : ICrud<int, Race>
    {
    }
}
