// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlayerRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the IPlayerRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO.Players;

    /// <summary>
    ///   Defines the IPlayerRepository type.
    /// </summary>
    public interface IPlayerRepository : ICrud<int, Player>
    {
    }
}
