// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceScheduler.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The scheduler using Quarts.Net
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Space.Game;

    using global::Quartz;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;
    using Space.Scheduler.Jobs;

    /// <summary>
    /// The scheduler using Quarts.Net
    /// </summary>
    public class SpaceScheduler : ISpaceScheduler
    {
        /// <summary>
        /// The scheduler factory for getting schedulers.
        /// </summary>
        private readonly IScheduler scheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceScheduler"/> class. 
        /// </summary>
        /// <param name="scheduler">
        /// The scheduler factory.
        /// </param>
        public SpaceScheduler(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

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
        public bool BuildBuildings(Player player, Planet planet, BuildCosts<BuildingType> buildingCosts, NetValue costs, BuildingType type)
        {
            if (this.scheduler == null)
            {
                Trace.WriteLine("this.scheduler is null", "SpaceScheduler.SubtractBuildCosts");
                return false;
            }

            if (buildingCosts == null)
            {
                Trace.WriteLine("buildCosts is null", "SpaceScheduler.SubtractBuildCosts");
                return false;
            }

            // Needs to subtract the cost here. return false if the player tries to build too many buildings.
            var totalCosts = buildingCosts.CalculateBuildCosts(costs, planet.TotalBuildings, planet.BuildingCapacity);
            player.TotalNetValue.Subtract(totalCosts);

            var jobSetup = new JobSetup<BuildBuildingsJob>(this.scheduler);
            jobSetup.Set(bbj => bbj.PlanetID, planet.ID);
            jobSetup.Set(bbj => bbj.BuildingType, type);
            jobSetup.Set(bbj => bbj.BuildingCount, totalCosts.EntityCount);

            jobSetup.Run(DateTimeOffset.UtcNow.AddMilliseconds(buildingCosts.Time));
            
            return true;
        }
    }
}
