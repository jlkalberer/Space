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

    using Ninject;

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
        /// <param name="planet">
        /// The planet to build on.
        /// </param>
        /// <param name="player">
        /// The player to build for.
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
        public bool BuildBuildings(Planet planet, Player player, NetValue costs, BuildingType type)
        {
            if (this.scheduler == null)
            {
                return false;
            }

            var jobSetup = new JobSetup<BuildBuildingsJob>(this.scheduler);
            jobSetup.Set(bbj => bbj.PlanetID, planet.ID);
            jobSetup.Set(bbj => bbj.PlayerID, player.ID);
            jobSetup.Set(bbj => bbj.Costs, costs);
            jobSetup.Set(bbj => bbj.BuildingType, type);

            jobSetup.Run(new DateTimeOffset());
            
            return true;
        }
    }
}
