// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpatialEntityRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The SpatialEntity repository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO.Spatial;

    /// <summary>
    /// The SpatialEntity repository interface.
    /// </summary>
    public interface ISpatialEntityRepository : ICrud<int, SpatialEntity>
    {
    }
}
