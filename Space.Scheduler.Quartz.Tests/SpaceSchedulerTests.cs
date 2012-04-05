// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpaceSchedulerTests.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   All the tests for the Quartz.Net Scheduler
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz.Tests
{
    using System.Collections.ObjectModel;

    using Moq;

    using NUnit.Framework;

    using global::Quartz;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// All the tests for the Quartz.Net Scheduler
    /// </summary>
    public class SpaceSchedulerTests
    {
        /// <summary>
        /// Test the BuildBuildings method and makes sure that 
        /// </summary>
        [TestFixture]
        public class TheBuildBuildingsMethod
        {
            /// <summary>
            /// Test to make sure that the build fails if there is no scheduler.
            /// </summary>
            [Test]
            public void WillNotBuildBuildingHasNoSchedulerFactoryTest()
            {
                var spaceScheduler = new SpaceScheduler(null);

                Assert.IsFalse(spaceScheduler.BuildBuildings(null, null, null, default(BuildingType)));
            }

            /// <summary>
            /// Test to make sure that the build succeeds.
            /// </summary>
            [Test]
            public void WillNotBuildBuildingBecauseGalaxyDontExistTest()
            {
                var scheduler = new Mock<IScheduler>();
                var spaceScheduler = new SpaceScheduler(scheduler.Object);
                var player = new Player();

                Assert.IsFalse(spaceScheduler.BuildBuildings(null, player, null, default(BuildingType)));
            }

            /// <summary>
            /// Test to make sure that the build succeeds.
            /// </summary>
            [Test]
            public void WillNotBuildBuildingBecauseGalaxySettingsDontExistTest()
            {
                var scheduler = new Mock<IScheduler>();
                var spaceScheduler = new SpaceScheduler(scheduler.Object);
                var player = new Player
                    {
                        Galaxy = new Galaxy()
                    };

                Assert.IsFalse(spaceScheduler.BuildBuildings(null, player, null, default(BuildingType)));
            }

            /// <summary>
            /// Test to make sure that the build succeeds.
            /// </summary>
            [Test]
            public void WillNotBuildBuildingBecauseBuildCostsDontExistTest()
            {
                var scheduler = new Mock<IScheduler>();
                var spaceScheduler = new SpaceScheduler(scheduler.Object);
                var player = new Player { Galaxy = new Galaxy { GalaxySettings = new GalaxySettings() } };

                Assert.IsFalse(spaceScheduler.BuildBuildings(null, player, null, default(BuildingType)));
            }
            
            /// <summary>
            /// Test to make sure that the build succeeds.
            /// </summary>
            [Test]
            public void WillNotBuildBuildingBecauseBuildCostDoesntExistTest()
            {
                var scheduler = new Mock<IScheduler>();
                var spaceScheduler = new SpaceScheduler(scheduler.Object);
                var player = new Player
                    {
                        Galaxy =
                            new Galaxy
                                {
                                    GalaxySettings =
                                        new GalaxySettings { BuildingCosts = new Collection<BuildingCosts>() }
                                }
                    };

                Assert.IsFalse(spaceScheduler.BuildBuildings(null, player, null, default(BuildingType)));
            }

            /// <summary>
            /// Test to make sure that the build succeeds.
            /// </summary>
            [Test]
            public void WillBuildBuildingTest()
            {
                var scheduler = new Mock<IScheduler>();
                var spaceScheduler = new SpaceScheduler(scheduler.Object);

                var planet = new Planet { ID = 1 };
                var player = new Player
                    {
                        ID = 1,
                        Galaxy =
                            new Galaxy
                                {
                                    GalaxySettings =
                                        new GalaxySettings
                                            {
                                                BuildingCosts =
                                                    new Collection<BuildingCosts>
                                                        {
                                                            new BuildingCosts { Type = default(BuildingType) } 
                                                        }
                                            }
                                }
                    };

                var costs = new NetValue
                    {
                        BuildingCount = 1,
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1
                    };

                Assert.IsTrue(spaceScheduler.BuildBuildings(planet, player, costs, default(BuildingType)));
            }
        }
    }
}
