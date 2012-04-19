// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DTOExtensionsTests.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Unit tests for Space.Game.DTOExtensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Game.Tests
{
    using System;

    using NUnit.Framework;

    using Space.DTO;
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// Unit tests for Space.Game.DTOExtensions.
    /// </summary>
    public class DTOExtensionsTests
    {
        /// <summary>
        /// Contains the tests for all Update overloads.
        /// </summary>
        [TestFixture]
        public class TheUpdateMethod
        {
            #region Update Planet

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

            /// <summary>
            /// Test to make sure that the population can grow.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetPopulationSize()
            {
                var planet = new Planet { BuildingCapacity = 10 };

                planet.Update(
                    new GalaxySettings { BasePopulation = 1, PopulationGrowth = 100, MaxPopulationPerBuildings = 1 },
                    new PlayerBonuses { WelfareBonus = 1 });

                Assert.AreEqual(planet.Population, 2);
            }

            /// <summary>
            /// Test to make sure that the population can be capped.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetPopulationSizeToPlanetMax()
            {
                var planet = new Planet();

                planet.Update(
                    new GalaxySettings { BasePopulation = 1, PopulationGrowth = 100 },
                    new PlayerBonuses { WelfareBonus = 1 });

                Assert.AreEqual(planet.Population, 1);
            }

            /// <summary>
            /// Test to make sure that the population growth is set.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetGrowth()
            {
                var planet = new Planet { BuildingCapacity = 10 };

                planet.Update(
                    new GalaxySettings { BasePopulation = 1, PopulationGrowth = 100, MaxPopulationPerBuildings = 1 },
                    new PlayerBonuses { WelfareBonus = 1 });

                Assert.AreEqual(planet.PopulationGrowth, 1);
            }

            /// <summary>
            /// Test that the PlanetValue output of CashFactoryCash is correct.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetCashFactoryCash()
            {
                var planet = new Planet { CashBonus = 1, CashFactoryCount = 1 };

                var galaxySettings = new GalaxySettings { CashOutput = 2 };

                var playerBonuses = new PlayerBonuses { EconomyBonus = 1 };

                var output = planet.Update(galaxySettings, playerBonuses);

                Assert.AreEqual(output.CashFactoryCash, 2);
            }

            /// <summary>
            /// Test that the PlanetValue output of PopulationCash is correct.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetPopulationCash()
            {
                var planet = new Planet { CashBonus = 1, Population = 1 };

                var galaxySettings = new GalaxySettings { PopulationCashDivider = 1, BasePopulation = 1 };

                var playerBonuses = new PlayerBonuses { EconomyBonus = 1 };

                var output = planet.Update(galaxySettings, playerBonuses);

                Assert.AreEqual(output.PopulationCash, 1);
            }

            /// <summary>
            /// Test that the PlanetValue output of TaxOfficeCash is correct.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetTaxOfficeCash()
            {
                var planet = new Planet { CashBonus = 1, TaxOfficeCount = 1 };

                var galaxySettings = new GalaxySettings { PopulationCashDivider = 1, PositiveIncomeCash = 1 };

                var playerBonuses = new PlayerBonuses { EconomyBonus = 1 };

                var output = planet.Update(galaxySettings, playerBonuses);

                Assert.AreEqual(output.TaxOfficeCash, 1);
            }

            /// <summary>
            /// Test that the PlanetValue output of Cash is correct.
            /// </summary>
            [Test]
            public void WillUpdateThePlanetCash()
            {
                var planet = new Planet { CashBonus = 1, CashFactoryCount = 1, Population = 1, TaxOfficeCount = 1 };

                var galaxySettings = new GalaxySettings
                    { CashOutput = 2, PopulationCashDivider = 1, BasePopulation = 1, PositiveIncomeCash = 1 };

                var playerBonuses = new PlayerBonuses { EconomyBonus = 1 };

                var output = planet.Update(galaxySettings, playerBonuses);

                Assert.AreEqual(output.Cash, 6);
            }

            /// <summary>
            /// Tests to verify that the planet is producing the correct amount of Energy.
            /// </summary>
            [Test]
            public void WillUpdatePlanetEnergy()
            {
                var planet = new Planet { EnergyLabCount = 1, EnergyBonus = 1 };
                var output = planet.Update(new GalaxySettings(), new PlayerBonuses());

                Assert.AreEqual(output.Energy, 1);
            }

            /// <summary>
            /// Tests to verify that the planet is producing the correct amount of Food.
            /// </summary>
            [Test]
            public void WillUpdatePlanetFood()
            {
                var planet = new Planet { FarmCount = 1, FoodBonus = 1 };
                var output = planet.Update(new GalaxySettings { FoodOutput = 1 }, new PlayerBonuses());

                Assert.AreEqual(output.Food, 1);
            }

            /// <summary>
            /// Tests to verify that the planet is producing the correct amount of Iron.
            /// </summary>
            [Test]
            public void WillUpdatePlanetIron()
            {
                var planet = new Planet { MineCount = 1, IronBonus = 1 };
                var output = planet.Update(new GalaxySettings(), new PlayerBonuses());

                Assert.AreEqual(output.Iron, 1);
            }

            /// <summary>
            /// Tests to verify that the planet is producing the correct amount of Mana.
            /// </summary>
            [Test]
            public void WillUpdatePlanetMana()
            {
                var planet = new Planet { ManaCount = 1, ManaBonus = 1 };
                var output = planet.Update(new GalaxySettings(), new PlayerBonuses());

                Assert.AreEqual(output.Mana, 1);
            }

            /// <summary>
            /// Tests to verify that the planet is producing the correct amount of Research.
            /// </summary>
            [Test]
            public void WillUpdatePlanetResearch()
            {
                var planet = new Planet { ResearchLabCount = 1, ResearchBonus = 1 };
                var output = planet.Update(
                    new GalaxySettings { ResearchOutput = 1 }, new PlayerBonuses { ResearchBonus = 1 });

                Assert.AreEqual(output.Research, 1);
            }

            #endregion

            #region Update Player

            /// <summary>
            /// Test to make sure that the DecayedCash value is being properly set.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickDecayedCash()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Cash = 10 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.1 });

                Assert.AreEqual(player.TickValue.DecayedCash, 1);
            }

            /// <summary>
            /// Test to make sure that the DecayedCash value is being properly set.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickDecayedEnergy()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Energy = 10 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.1 });

                Assert.AreEqual(player.TickValue.DecayedEnergy, 1);
            }

            /// <summary>
            /// Test to make sure that the DecayedCash value is being properly set.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickDecayedFood()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Food = 10 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.1 });

                Assert.AreEqual(player.TickValue.DecayedFood, 1);
            }

            /// <summary>
            /// Test to make sure that the DecayedCash value is being properly set.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickDecayedIron()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Iron = 10 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.1 });

                Assert.AreEqual(player.TickValue.DecayedIron, 1);
            }

            /// <summary>
            /// Test to make sure that the DecayedCash value is being properly set.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickDecayedMana()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Mana = 10 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.1 });

                Assert.AreEqual(player.TickValue.DecayedMana, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick buildings are updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickBuildings()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { EntityCount = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.Buildings, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick units are updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickUnits()
            {
                var player = new Player { UnitCount = 1, TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue();

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.Units, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Cash Factory Cash is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickCashFactoryCash()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { CashFactoryCash = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedCashFactoryCash, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Population cash is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickPopulationCash()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { PopulationCash = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedPopulationCash, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Tax Office Cash is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickTaxOfficeCash()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { TaxOfficeCash = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedTaxOfficeCash, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Cash is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickCash()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { Cash = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedCash, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Energy is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickEnergy()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { Energy = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedEnergy, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Food is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickFood()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { Food = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedFood, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Iron is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickIron()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { Iron = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedIron, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Population is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickPopulation()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { Population = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedPopulation, 1);
            }

            /// <summary>
            /// Test to make sure that the player Tick Research is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdateThePlayerTickResearch()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                var totalPlanetValue = new PlanetValue { Research = 1 };

                player.Update(totalPlanetValue, new GalaxySettings());

                Assert.AreEqual(player.TickValue.ProducedResearch, 1);
            }

            /// <summary>
            /// Test to make sure that the Total Cash is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalCash()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Cash = 2 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.5 });

                Assert.AreEqual(player.TotalNetValue.Cash, 1);
            }

            /// <summary>
            /// Test to make sure that the Total Energy is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalEnergy()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Energy = 2 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.5 });

                Assert.AreEqual(player.TotalNetValue.Energy, 1);
            }

            /// <summary>
            /// Test to make sure that the Total Food is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalFood()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Food = 2 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.5 });

                Assert.AreEqual(player.TotalNetValue.Food, 1);
            }

            /// <summary>
            /// Test to make sure that the Total Iron is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalIron()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Iron = 2 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.5 });

                Assert.AreEqual(player.TotalNetValue.Iron, 1);
            }

            /// <summary>
            /// Test to make sure that the Total Mana is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalMana()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Mana = 2 } };

                player.Update(new PlanetValue(), new GalaxySettings { Decay = 0.5 });

                Assert.AreEqual(player.TotalNetValue.Mana, 1);
            }

            /// <summary>
            /// Test to make sure that the Total is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotal()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue() };

                player.Update(
                    new PlanetValue
                        {
                            EntityCount = 1,
                            Cash = 2,

                            // since 1 is removed for having the building
                            Energy = 1,
                            Food = 1,
                            Iron = 1,
                            Mana = 1,
                            Networth = 1,
                            Population = 1,
                            Research = 1
                        },
                    new GalaxySettings());

                Assert.AreEqual(player.TotalNetValue.EntityCount, 1, "Building Count");
                Assert.AreEqual(player.TotalNetValue.Cash, 1, "Cash");
                Assert.AreEqual(player.TotalNetValue.Energy, 1, "Energy");
                Assert.AreEqual(player.TotalNetValue.Food, .9, "Food");
                Assert.AreEqual(player.TotalNetValue.Iron, 1, "Iron");
                Assert.AreEqual(player.TotalNetValue.Mana, 1, "Mana");
                Assert.AreEqual(player.TotalNetValue.Networth, 0, "Networth");
                Assert.AreEqual(player.TotalNetValue.Population, 1, "Population");
                Assert.AreEqual(player.TotalNetValue.Research, 1, "Research");
            }

            /// <summary>
            /// Test to make sure that the Total Population is updated accordingly.
            /// This should be set to the PlanetValue, not added.  If it is added the population will
            /// more than double each tick.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalPopulation()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Population = 1 } };

                player.Update(new PlanetValue { Population = 1 }, new GalaxySettings());

                Assert.AreEqual(player.TotalNetValue.Population, 1);
            }

            /// <summary>
            /// Test to make sure that the income when the player is starving is updated accordingly.
            /// </summary>
            [Test]
            public void WillUpdatePlayerIsStarving()
            {
                var player = new Player { TickValue = new TickValue(), TotalNetValue = new NetValue { Cash = 1 } };

                player.Update(new PlanetValue { Cash = 2, Population = 1 }, new GalaxySettings());

                // Check to make sure it is equal to 2 since we started with 1 cash
                Assert.AreEqual(player.TotalNetValue.Cash, 2, "Cash");
                Assert.AreEqual(player.TotalNetValue.Food, 0, "Food");
                Assert.IsTrue(player.TickValue.IsPopulationStarving, "Is starving");
            }

            /// <summary>
            /// Test to make sure that the income subtracts buildings and units from cost.
            /// </summary>
            [Test]
            public void WillUpdatePlayerTotalCashMinusUnitsAndBuildings()
            {
                var player = new Player
                    {
                        TickValue = new TickValue(),
                        TotalNetValue = new NetValue { Cash = 3, EntityCount = 1 },
                        UnitCount = 1
                    };

                player.Update(new PlanetValue(), new GalaxySettings());

                Assert.AreEqual(player.TotalNetValue.Cash, 1);
            }

            #endregion Update Player
        }

        /// <summary>
        /// Contains the tests for all Add overloads.
        /// </summary>
        [TestFixture]
        public class TheAddMethod
        {
            /// <summary>
            /// Test to make sure two NetValue objects are correctly added together.
            /// </summary>
            [Test]
            public void WillAddNetValue()
            {
                var nv1 = new NetValue
                    {
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1,
                        Population = 1,
                        Research = 1,
                        EntityCount = 1
                    };

                var nv2 = new NetValue
                    {
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1,
                        Population = 1,
                        Research = 1,
                        EntityCount = 1
                    };

                nv1.Add(nv2);

                Assert.AreEqual(nv1.Cash, 2, "Cash");
                Assert.AreEqual(nv1.Energy, 2, "Energy");
                Assert.AreEqual(nv1.Food, 2, "Food");
                Assert.AreEqual(nv1.Iron, 2, "Iron");
                Assert.AreEqual(nv1.Mana, 2, "Mana");
                Assert.AreEqual(nv1.Population, 2, "Population");
                Assert.AreEqual(nv1.Research, 2, "Research");
                Assert.AreEqual(nv1.EntityCount, 2, "EntityCount");
            }

            /// <summary>
            /// Test to make sure two PlanetValue objects are correctly added together.
            /// </summary>
            [Test]
            public void WillAddPlanetValue()
            {
                var nv1 = new PlanetValue
                    {
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1,
                        Population = 1,
                        Research = 1,
                        EntityCount = 1,
                        CashFactoryCash = 1,
                        PopulationCash = 1,
                        TaxOfficeCash = 1,
                    };

                var nv2 = new PlanetValue
                    {
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1,
                        Population = 1,
                        Research = 1,
                        EntityCount = 1,
                        CashFactoryCash = 1,
                        PopulationCash = 1,
                        TaxOfficeCash = 1,
                    };

                nv1.Add(nv2);

                Assert.AreEqual(nv1.Cash, 2, "Cash");
                Assert.AreEqual(nv1.Energy, 2, "Energy");
                Assert.AreEqual(nv1.Food, 2, "Food");
                Assert.AreEqual(nv1.Iron, 2, "Iron");
                Assert.AreEqual(nv1.Mana, 2, "Mana");
                Assert.AreEqual(nv1.Population, 2, "Population");
                Assert.AreEqual(nv1.Research, 2, "Research");
                Assert.AreEqual(nv1.EntityCount, 2, "EntityCount");
                Assert.AreEqual(nv1.CashFactoryCash, 2, "CashFactoryCash");
                Assert.AreEqual(nv1.PopulationCash, 2, "PopulationCash");
                Assert.AreEqual(nv1.TaxOfficeCash, 2, "TaxOfficeCash");
            }
        }

        /// <summary>
        /// Contains the tests for all Add overloads.
        /// </summary>
        [TestFixture]
        public class TheSubtractMethod
        {
            /// <summary>
            /// Test to make sure two NetValue objects are correctly subtracted together.
            /// </summary>
            [Test]
            public void WillSubtractNetValue()
            {
                var nv1 = new NetValue
                    {
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1,
                        Population = 1,
                        Research = 1,
                        EntityCount = 1
                    };

                var nv2 = new NetValue
                    {
                        Cash = 1,
                        Energy = 1,
                        Food = 1,
                        Iron = 1,
                        Mana = 1,
                        Population = 1,
                        Research = 1,
                        EntityCount = 1
                    };

                nv1.Subtract(nv2);

                Assert.AreEqual(nv1.Cash, 0, "Cash");
                Assert.AreEqual(nv1.Energy, 0, "Energy");
                Assert.AreEqual(nv1.Food, 0, "Food");
                Assert.AreEqual(nv1.Iron, 0, "Iron");
                Assert.AreEqual(nv1.Mana, 0, "Mana");
                Assert.AreEqual(nv1.Population, 0, "Population");
                Assert.AreEqual(nv1.Research, 0, "Research");
                Assert.AreEqual(nv1.EntityCount, 0, "EntityCount");
            }
        }

        /// <summary>
        /// Contains the tests for finding the maximum number of units/buildings.
        /// </summary>
        [TestFixture]
        public class TheCalculateBuildCostsMethod
        {
            /// <summary>
            /// Test to make sure that 0 items can be build when the player has no resources.
            /// </summary>
            [Test]
            public void WillReturnZeroBuildItems()
            {
                var totalNetValue = new NetValue();
                var buildCosts = new BuildCosts<object>();

                var netValue = buildCosts.CalculateBuildCosts(totalNetValue, 0, 1);

                Assert.AreEqual(netValue.EntityCount, 0);
            }

            /// <summary>
            /// Test to make sure that 1 items can be build when the player has resources that match the build costs.
            /// We are also building less than the build capacity of the planet so there are no increased costs.
            /// </summary>
            [Test]
            public void WillReturnOneBuildItem()
            {
                var totalNetValue = new NetValue { Cash = 1, Energy = 1, Food = 1, Iron = 1, Mana = 1 };
                var buildCosts = new BuildCosts<object> { Cash = 1, Energy = 1, Food = 1, Iron = 1, Mana = 1 };

                var netValue = buildCosts.CalculateBuildCosts(totalNetValue, 0, 1);

                Assert.AreEqual(netValue.EntityCount, 1);
            }

            /// <summary>
            /// Test to make sure that 1 items can be build when the player has resources.
            /// We increased the resources the player has but it is still one because of increased 
            /// build costs based on building capacity.
            /// </summary>
            [Test]
            public void WillTestBuildingCapacityMinimum()
            {
                var totalNetValue = new NetValue { Cash = 2, Energy = 2, Food = 2, Iron = 2, Mana = 2 };
                var buildCosts = new BuildCosts<object> { Cash = 1, Energy = 1, Food = 1, Iron = 1, Mana = 1 };

                var netValue = buildCosts.CalculateBuildCosts(totalNetValue, 0, 1);

                Assert.AreEqual(netValue.EntityCount, 1);
            }

            /// <summary>
            /// Test to make sure that 2 items can be build when the player has resources.
            /// We increased the resources the player has but it returns two builds since 
            /// The increased resources match the increased expenses based on building count.
            /// </summary>
            [Test]
            public void WillTestBuildingCapacityMaximum()
            {
                var totalNetValue = new NetValue { Cash = 3, Energy = 3, Food = 3, Iron = 3, Mana = 3 };
                var buildCosts = new BuildCosts<object> { Cash = 1, Energy = 1, Food = 1, Iron = 1, Mana = 1 };

                var netValue = buildCosts.CalculateBuildCosts(totalNetValue, 0, 1);

                // 2 here because of the building capacity and overbuild cost (x2)
                Assert.AreEqual(netValue.EntityCount, 2);
            }

            /// <summary>
            /// Test to make that more than one building can be built with a random amount of resources
            /// </summary>
            [Test]
            public void WillTestBuildingCapacityWithLargeNumbers()
            {
                var r = new Random();

                var totalNetValue = new NetValue
                    {
                        Cash = r.Next(1, int.MaxValue),
                        Energy = r.Next(1, int.MaxValue),
                        Food = r.Next(1, int.MaxValue),
                        Iron = r.Next(1, int.MaxValue),
                        Mana = r.Next(1, int.MaxValue)
                    };
                var buildCosts = new BuildCosts<object>
                    {
                        Cash = 1 * r.NextDouble(),
                        Energy = 1 * r.NextDouble(),
                        Food = 1 * r.NextDouble(),
                        Iron = 1 * r.NextDouble(),
                        Mana = 1 * r.NextDouble()
                    };

                var netValue = buildCosts.CalculateBuildCosts(totalNetValue, 0, r.Next(250));

                Assert.GreaterOrEqual(netValue.EntityCount, 1);
            }


            /// <summary>
            /// Test to that the method will return the correct number of buildings based on the NetValue.EntityCount
            /// passed in.  This is used when a user wants to build less than their maximum number of buildings.
            /// </summary>
            [Test]
            public void WillTestUser()
            {
                var totalNetValue = new NetValue { EntityCount = 2, Cash = 3, Energy = 3, Food = 3, Iron = 3, Mana = 3 };
                var buildCosts = new BuildCosts<object> { Cash = 1, Energy = 1, Food = 1, Iron = 1, Mana = 1 };

                var netValue = buildCosts.CalculateBuildCosts(totalNetValue, 0, 100);

                Assert.AreEqual(netValue.EntityCount, 2);
            }
        }

        /// <summary>
        /// Contains tests for building buildings/units.
        /// </summary>
        [TestFixture]
        public class TheSubtractBuildCostsMethod
        {
            /// <summary>
            /// Test to make sure that the players cannot build if the EntityCount is 0.
            /// </summary>
            [Test]
            public void WillNotSubtractCorrectCostsBecauseThereAreNoBuildingsToBuild()
            {
                var player = new Player { TotalNetValue = new NetValue { Cash = 2, Energy = 2, Food = 2, Iron = 2, Mana = 2 } };
                var costs = new NetValue { Cash = 1, Energy = 1, Food = 1, Iron = 1, Mana = 1 };

                Assert.IsFalse(player.SubtractBuildCosts(costs));
            }

            /// <summary>
            /// Test to make sure that the players resources are correctly deleted.
            /// </summary>
            [Test]
            public void WillSubtractCorrectCosts()
            {
                var player = new Player
                    { TotalNetValue = new NetValue { Cash = 2, Energy = 2, Food = 2, Iron = 2, Mana = 2 } };
                var costs = new NetValue { EntityCount = 2, Cash = 2, Energy = 2, Food = 2, Iron = 2, Mana = 2 };

                player.SubtractBuildCosts(costs);

                var totalNetValue = player.TotalNetValue;
                Assert.AreEqual(totalNetValue.Cash, 0);
                Assert.AreEqual(totalNetValue.Energy, 0);
                Assert.AreEqual(totalNetValue.Food, 0);
                Assert.AreEqual(totalNetValue.Iron, 0);
                Assert.AreEqual(totalNetValue.Mana, 0);
            }
        }

        /// <summary>
        /// Contains test for adding buildings.
        /// </summary>
        [TestFixture]
        public class TheAddBuildingsMethod
        {
            /// <summary>
            /// Test to make sure that the total number of buildings a player has is updated.
            /// </summary>
            [Test]
            public void WillAddToBuildingCount()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };
                const BuildingType Type = BuildingType.CashFactory;

                planet.AddBuildings(player, 5, Type);

                Assert.AreEqual(player.TotalNetValue.EntityCount, 5);
            }

            /// <summary>
            /// Test to make sure that Cash Factories are built on the planet.
            /// </summary>
            [Test]
            public void WillAddCashFactorys()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.CashFactory;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.CashFactoryCount, 1);
            }

            /// <summary>
            /// Test to make sure that EnergyLabs are built on the planet.
            /// </summary>
            [Test]
            public void WillAddEnergyLabs()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.EnergyLab;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.EnergyLabCount, 1);
            }

            /// <summary>
            /// Test to make sure that Farms are built on the planet.
            /// </summary>
            [Test]
            public void WillAddFarms()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.Farm;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.FarmCount, 1);
            }

            /// <summary>
            /// Test to make sure that Lasers are built on the planet.
            /// </summary>
            [Test]
            public void WillAddLasers()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.Laser;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.LaserCount, 1);
            }

            /// <summary>
            /// Test to make sure that Living Quarters are built on the planet.
            /// </summary>
            [Test]
            public void WillAddLivingQuarters()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.LivingQuarters;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.LivingQuartersCount, 1);
            }

            /// <summary>
            /// Test to make sure that Mana buildings are built on the planet.
            /// </summary>
            [Test]
            public void WillAddMana()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.Mana;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.ManaCount, 1);
            }

            /// <summary>
            /// Test to make sure that Mines are built on the planet.
            /// </summary>
            [Test]
            public void WillAddMines()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.Mine;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.MineCount, 1);
            }

            /// <summary>
            /// Test to make sure that a Portal is built on the planet.
            /// </summary>
            [Test]
            public void WillAddPortal()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.Portal;

                planet.AddBuildings(player, 1, Type);

                Assert.IsTrue(planet.HasPortal);
            }

            /// <summary>
            /// Test to make sure that Research Labs are built on the planet.
            /// </summary>
            [Test]
            public void WillAddResearchLabs()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.ResearchLab;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.ResearchLabCount, 1);
            }

            /// <summary>
            /// Test to make sure that Tax Offices are built on the planet.
            /// </summary>
            [Test]
            public void WillAddTaxOffice()
            {
                var planet = new Planet();
                var player = new Player { TotalNetValue = new NetValue() };

                const BuildingType Type = BuildingType.TaxOffice;

                planet.AddBuildings(player, 1, Type);

                Assert.AreEqual(planet.TaxOfficeCount, 1);
            }
        }
    }
}