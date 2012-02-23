// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildingCosts.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Class to house building costs -- must do this in order for EntityFramework to function correctly...
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    using Buildings;

    /// <summary>
    /// Class to house building costs -- must do this in order for EntityFramework to function correctly...
    /// </summary>
    public class BuildingCosts : BuildCosts<BuildingType>
    {
    }
}
