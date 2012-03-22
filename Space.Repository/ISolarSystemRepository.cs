// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISolarSystemRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the ISolarSystemRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO.Spatial;

    /// <summary>
    ///   Defines the ISolarSystemRepository type.
    /// </summary>
    public interface ISolarSystemRepository : ICrud<int, SolarSystem>
    {
    }
}
