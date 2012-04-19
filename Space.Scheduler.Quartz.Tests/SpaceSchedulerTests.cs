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
    using Moq;

    using NUnit.Framework;

    using global::Quartz;

    using Space.DTO;
    using Space.DTO.Buildings;
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
            public void WillNotBuildBuildingBecauseBuildCostDoesntExistTest()
            {
                var scheduler = new Mock<IScheduler>();
                var spaceScheduler = new SpaceScheduler(scheduler.Object);

                Assert.IsFalse(spaceScheduler.BuildBuildings(null, null, null, default(BuildingType)));
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
                var buildCosts = new BuildingCosts { Type = default(BuildingType) };

                var costs = new NetValue
                    {
                        EntityCount = 1,
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1
                    };

                Assert.IsTrue(spaceScheduler.BuildBuildings(planet, buildCosts, costs, default(BuildingType)));
            }
        }
    }
}
