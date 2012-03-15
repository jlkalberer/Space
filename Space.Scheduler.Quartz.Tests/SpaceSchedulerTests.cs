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

    using Space.DTO.Buildings;

    using global::Quartz;

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
            public void WillBuildBuildingHasNoSchedulerFactoryTest()
            {
                var spaceScheduler = new SpaceScheduler(null);

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

                Assert.IsTrue(spaceScheduler.BuildBuildings(null, null, null, default(BuildingType)));
            }
        }
    }
}
