// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnit.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The interface used for any type of unit.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Entities
{
    /// <summary>
    /// The interface used for any type of unit.
    /// </summary>
    public interface IUnit : IDataObject<int>, ISpatialEntity
    {
        /// <summary>
        /// Gets or sets FleetID used as a primary key to the current fleet.
        /// </summary>
        int? FleetID { get; set; }

        /// <summary>
        /// Updates the current unit.
        /// </summary>
        void Update();
    }
}
