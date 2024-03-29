﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGalaxyRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The repository interface used for Galaxies.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using Space.DTO.Spatial;

    /// <summary>
    /// The repository interface used for Galaxies.
    /// </summary>
    public interface IGalaxyRepository : ICrud<int, Galaxy>, IEager<int, Galaxy>
    {
    }
}
