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
                Trace.WriteLine("this.scheduler is null", "SpaceScheduler.BuildBuildings");
                return false;
            }

            if (player.Galaxy == null || player.Galaxy.GalaxySettings == null)
            {
                Trace.WriteLine("player.Galaxy is null", "SpaceScheduler.BuildBuildings");
                return false;
            }
            
            if (player.Galaxy.GalaxySettings == null)
            {
                Trace.WriteLine("player.Galaxy.GalaxySettings is null", "SpaceScheduler.BuildBuildings");
                return false;
            }

            if (player.Galaxy.GalaxySettings.BuildingCosts == null)
            {
                Trace.WriteLine("player.Galaxy.GalaxySettings.BuildingCosts is null", "SpaceScheduler.BuildBuildings");
                return false;
            }

            var buildCosts = player.Galaxy.GalaxySettings.BuildingCosts.FirstOrDefault(bc => bc.Type == type);
            if (buildCosts == null)
            {
                Trace.WriteLine("buildCosts is null", "SpaceScheduler.BuildBuildings");
                return false;
            }

            var jobSetup = new JobSetup<BuildBuildingsJob>(this.scheduler);
            jobSetup.Set(bbj => bbj.PlanetID, planet.ID);
            jobSetup.Set(bbj => bbj.BuildingType, type);

            jobSetup.Run(DateTimeOffset.UtcNow.AddMilliseconds(buildCosts.Time));
            
            return true;
        }
    }
}
