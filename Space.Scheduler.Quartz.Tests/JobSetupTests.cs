// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobSetupTests.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The Test for the JobSetup class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz.Tests
{
    using NUnit.Framework;

    using Space.Scheduler.Jobs;

    /// <summary>
    /// The Test for the JobSetup class.
    /// </summary>
    public class JobSetupTests
    {
        /// <summary>
        /// The method to set the 
        /// </summary>
        [TestFixture]
        public class TheGetAndSetMethod
        {
            /// <summary>
            /// Test to verify that the Set method is working appropriately.
            /// </summary>
            [Test]
            public void WillSucceedOnSettingProperty()
            {
                var setup = new JobSetup<BuildBuildingsJob>(null);
                setup.Set(bbj => bbj.PlanetID, 10);

                Assert.AreEqual(setup.Get(bbj => bbj.PlanetID), 10);
            }
        }
    }
}
