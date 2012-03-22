// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlanetRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the IPlanetRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO.Spatial;

    /// <summary>
    /// Defines the IPlanetRepository type.
    /// </summary>
    public interface IPlanetRepository : ICrud<int, Planet>
    {
    }
}
