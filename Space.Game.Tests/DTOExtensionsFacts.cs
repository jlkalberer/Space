// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DTOExtensionsFacts.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Unit tests for Space.Game.DTOExtensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Game.Tests
{
    using NUnit.Framework;

    using Space.DTO;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// Unit tests for Space.Game.DTOExtensions.
    /// </summary>
    public class DTOExtensionsFacts
    {
        /// <summary>
        /// Contains the tests for all Update overloads
        /// </summary>
        [TestFixture]
        public class TheUpdateMethod
        {
            /// <summary>
            /// Tests to make sure that the building count is updated when the method is run.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetBuildingCount()
            {
                var planet = new Planet
                    {
                        CashFactoryCount = 1,
                        EnergyLabCount = 1,
                        FarmCount = 1,
                        LaserCount = 1,
                        LivingQuartersCount = 1,
                        ManaCount = 1,
                        MineCount = 1,
                        ResearchLabCount = 1,
                        TaxOfficeCount = 1,
                        HasPortal = true
                    };

                planet.Update(new GalaxySettings(), new PlayerBonuses());

                Assert.AreEqual(planet.TotalBuildings, 10);
            }

            /// <summary>
            /// Tests to make sure that the building count is updated when the method is run.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetPopulationIfZeroCount()
            {
                var planet = new Planet();

                planet.Update(new GalaxySettings { BasePopulation = 1 }, new PlayerBonuses());

                Assert.AreEqual(planet.Population, 1);
            }
        }
    }
}
