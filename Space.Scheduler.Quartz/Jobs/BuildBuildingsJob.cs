// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildBuildingsJob.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A job to create a building.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz.Jobs
{
    using System;

    using Space.Game;
    using Space.Repository;

    using global::Quartz;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// A job to create a building.
    /// </summary>
    public class BuildBuildingsJob : IJob
    {
        /// <summary>
        /// Access to Planet objects.
        /// </summary>
        private readonly IPlanetRepository planetRepository;

        /// <summary>
        /// Access to Player objects.
        /// </summary>
        private readonly IPlayerRepository playerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildBuildingsJob"/> class.
        /// </summary>
        /// <param name="planetRepository">
        /// The planet repository.
        /// </param>
        /// <param name="playerRepository">
        /// The player Repository.
        /// </param>
        public BuildBuildingsJob(IPlanetRepository planetRepository, IPlayerRepository playerRepository)
        {
            this.planetRepository = planetRepository;
            this.playerRepository = playerRepository;
        }

        #region Implementation of IJob

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler"/> when a <see cref="T:Quartz.ITrigger"/>
        ///             fires that is associated with the <see cref="T:Quartz.IJob"/>.
        /// </summary>
        /// <remarks>
        /// The implementation may wish to set a  result object on the 
        ///             JobExecutionContext before this method exits.  The result itself
        ///             is meaningless to Quartz, but may be informative to 
        ///             <see cref="T:Quartz.IJobListener"/>s or 
        ///             <see cref="T:Quartz.ITriggerListener"/>s that are watching the job's 
        ///             execution.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        public void Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;

            var planetID = dataMap.GetInt("PlanetID");
            if (planetID == default(int))
            {
                return;
            }

            var planet = this.planetRepository.Get(planetID);

            var playerID = dataMap.GetInt("PlayerID");
            if (playerID == default(int))
            {
                return;
            }

            var player = this.playerRepository.Get(playerID);

            var costs = dataMap.Get("Costs") as NetValue;
            if (costs == null)
            {
                return;
            }

            BuildingType buildingType;
            if (!Enum.TryParse(dataMap.GetString("BuildingType"), true, out buildingType))
            {
                return;
            }

            planet.BuildBuildings(player, costs, buildingType);
            this.planetRepository.SaveChanges();
        }

        #endregion
    }
}
