// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGalaxySettingsRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The repository interface used for Galaxy Settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO;

    /// <summary>
    /// The repository interface used for Galaxy Settings.
    /// </summary>
    public interface IGalaxySettingsRepository : ICrud<int, GalaxySettings>, IEager<int, GalaxySettings>
    {
    }
}
