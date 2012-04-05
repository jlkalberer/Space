// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildBuildingsJobTest.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Tests for the BuildBuildingsJob.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Tests.Jobs
{
    using Moq;

    using NUnit.Framework;

    using Space.DTO;
    using Space.DTO.Players;
    using Space.DTO.Spatial;
    using Space.Repository;
    using Space.Scheduler.Jobs;

    /// <summary>
    /// Tests for the BuildBuildingsJob.
    /// </summary>
    public class BuildBuildingsJobTest
    {
        /// <summary>
        /// Testing for the Execute method which runs when the job is scheduled to fire.
        /// </summary>
        [TestFixture]
        public class TheExecuteMethod
        {
            /// <summary>
            /// Test that the function returns when the planet ID is not passed in.
            /// </summary>
            [Test]
            public void PlanetIDDoesntExist()
            {
                var planetRepository = new Mock<IPlanetRepository>();
                var job = new BuildBuildingsJob(planetRepository.Object, null);

                job.Run();

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Never());
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Never());
            }

            /// <summary>
            /// Test that the function returns when the planet ID is not passed in.
            /// </summary>
            [Test]
            public void PlayerIDDoesntExist()
            {
                var planetRepository = new Mock<IPlanetRepository>();
                var playerRepository = new Mock<IPlayerRepository>();

                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object) { PlanetID = 1 };

                job.Run();

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Never());
            }

            /// <summary>
            /// Test that the function returns when the planet ID is not passed in.
            /// </summary>
            [Test]
            public void BuildingCountDoesntExist()
            {
                var planetRepository = new Mock<IPlanetRepository>();
                var playerRepository = new Mock<IPlayerRepository>();
                playerRepository.Setup(pr => pr.Get(It.IsAny<int>())).Returns(new Player());

                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object)
                    { PlanetID = 1, PlayerID = 1 };

                job.Run();

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Never());
            }
            
            /// <summary>
            /// Test that the function returns when the planet ID is not passed in.
            /// </summary>
            [Test]
            public void WillBuildBuildings()
            {
                var planetRepository = new Mock<IPlanetRepository>();
                var playerRepository = new Mock<IPlayerRepository>();

                planetRepository.Setup(pr => pr.Get(It.IsAny<int>())).Returns(new Planet());
                playerRepository.Setup(pr => pr.Get(It.IsAny<int>())).Returns(new Player
                    {
                        TotalNetValue = new NetValue()
                    });

                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object)
                    { PlanetID = 1, PlayerID = 1, BuildingCount = 1 };

                job.Run();

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Once());
            }
        }
    }
}
