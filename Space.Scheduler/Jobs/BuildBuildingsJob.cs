// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildBuildingsJob.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A job to create a building.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Jobs
{
    using Space.DTO.Buildings;
    using Space.Game;
    using Space.Repository;

    /// <summary>
    /// A job to create a building.
    /// </summary>
    public class BuildBuildingsJob : IGameJob
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

        /// <summary>
        /// Gets or sets PlanetID.
        /// </summary>
        public int PlanetID { get; set; }

        /// <summary>
        /// Gets or sets PlayerID.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets Costs.
        /// </summary>
        public int BuildingCount { get; set; }

        /// <summary>
        /// Gets or sets BuildingType.
        /// </summary>
        public BuildingType BuildingType { get; set; }

        #region Implementation of IGameJob

        /// <summary>
        /// Runs code specific to the game.
        /// </summary>
        public void Run()
        {
            if (this.PlanetID == default(int))
            {
                return;
            }

            var planet = this.planetRepository.Get(this.PlanetID);

            if (this.PlayerID == default(int))
            {
                return;
            }

            var player = this.playerRepository.Get(this.PlayerID);

            if (this.BuildingCount == default(int))
            {
                return;
            }

            planet.AddBuildings(player, this.BuildingCount, this.BuildingType);
            this.planetRepository.SaveChanges();
        }

        #endregion
    }
}
