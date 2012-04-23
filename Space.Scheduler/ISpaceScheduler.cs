// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpaceScheduler.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The basic scheduling interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Space.Scheduler
{
    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// The basic scheduling interface.  
    /// </summary>
    public interface ISpaceScheduler
    {
        /// <summary>
        /// Used to schedule buildings to be built.
        /// </summary>
        /// <param name="player">
        /// The player who is building.
        /// </param>
        /// <param name="planet">
        /// The planet to build on.
        /// </param>
        /// <param name="buildingCosts">
        /// The building costs for a particular building type.
        /// </param>
        /// <param name="costs">
        /// The costs of building.
        /// </param>
        /// <param name="type">
        /// The type of building to build.
        /// </param>
        /// <returns>
        /// True if the build was added to the queue.
        /// </returns>
        bool BuildBuildings(
            Player player, Planet planet, BuildCosts<BuildingType> buildingCosts, NetValue costs, BuildingType type);
    }
}
