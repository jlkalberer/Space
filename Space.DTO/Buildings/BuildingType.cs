// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildingType.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A list of all building types
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Buildings
{
    /// <summary>
    /// A list of all building types
    /// </summary>
    public enum BuildingType
    {
        /// <summary>
        /// Building type for Cash Factories.
        /// </summary>
        CashFactory = 0,

        /// <summary>
        /// Building type for Energy Labs.
        /// </summary>
        EnergyLab = 1,

        /// <summary>
        /// Building type for Farms.
        /// </summary>
        Farm,

        /// <summary>
        /// Building type for Lasers.
        /// </summary>
        Laser,

        /// <summary>
        /// Building type for Living Quarters.
        /// </summary>
        LivingQuarters,

        /// <summary>
        /// Building type for Mines.
        /// </summary>
        Mine,

        /// <summary>
        /// Building type for Portals.
        /// </summary>
        Portal,

        /// <summary>
        /// Building type for Mana producers.
        /// </summary>
        Mana,

        /// <summary>
        /// Building type for Research Labs.
        /// </summary>
        ResearchLab,

        /// <summary>
        /// Building type for Tax Offices.
        /// </summary>
        TaxOffice
    }
}
