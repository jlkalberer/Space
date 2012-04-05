// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTests.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Contains tests for all functions of the Game class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Game.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using Space.DTO;
    using Space.DTO.Players;
    using Space.DTO.Spatial;
    using Space.Repository;

    /// <summary>
    /// Contains tests for all functions of the Game class.
    /// </summary>
    public class GameTests
    {
        /// <summary>
        /// Test for the update method.  Verifies that all players and planets are updated and saved to the repository.
        /// </summary>
        [TestFixture]
        public class TheUpdateMethod
        {
            /// <summary>
            /// Check to make sure all planets are grouped together.
            /// </summary>
            [Test]
            public void PlanetHasNoOwner()
            {
                var playerRepository = new Mock<IPlayerRepository>();

                var planetRepository = new Mock<IPlanetRepository>();
                planetRepository.Setup(pr => pr.All).Returns(
                    new List<Planet> { new Planet() }.AsQueryable());

                var game = new Game(playerRepository.Object, null, null, null, planetRepository.Object, null);
                game.Update();

                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Never());
                playerRepository.Verify(pr => pr.SaveChanges(), Times.Once());
            }

            /// <summary>
            /// Test to make sure that if the player has been removed from the player repository,
            /// the planet will not be updated.  This code path should be impossible to hit.
            /// </summary>
            [Test]
            public void PlanetHasOwnerButPlayerNotInPlayerRepository()
            {
                var player = new Player { ID = 1 };
                var playerRepository = new Mock<IPlayerRepository>();
                playerRepository.Setup(pr => pr.Get(It.IsAny<int>()));

                var planetRepository = new Mock<IPlanetRepository>();
                planetRepository.Setup(pr => pr.All).Returns(
                    new List<Planet> { new Planet { Owner = player } }.AsQueryable());

                var game = new Game(playerRepository.Object, null, null, null, planetRepository.Object, null);
                game.Update();

                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.SaveChanges(), Times.Once());
            }

            /// <summary>
            /// Test to make sure that if the player has been removed from the player repository,
            /// the planet will not be updated.  This code path should be impossible to hit.
            /// </summary>
            [Test]
            public void PlanetHasOwner()
            {
                var player = new Player
                    {
                        ID = 1,
                        Galaxy = new Galaxy { GalaxySettings = new GalaxySettings() },
                        Race = new Race(),
                        ResearchPoints = new ResearchPoints(),
                        TickValue = new TickValue(),
                        TotalNetValue = new NetValue()
                    };

                var playerRepository = new Mock<IPlayerRepository>();
                playerRepository.Setup(pr => pr.Get(It.IsAny<int>())).Returns(player);

                var planetRepository = new Mock<IPlanetRepository>();
                planetRepository.Setup(pr => pr.All).Returns(
                    new List<Planet> { new Planet { Owner = player } }.AsQueryable());

                var game = new Game(playerRepository.Object, null, null, null, planetRepository.Object, null);
                game.Update();

                planetRepository.Verify(pr => pr.Update(It.IsAny<Planet>()), Times.Once());
                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.SaveChanges(), Times.Once());
            }
        }

        /// <summary>
        /// Test the method for creating a new galaxy.
        /// This method is a beast and it populates the galaxy with solar systems.
        /// </summary>
        [TestFixture]
        public class TheGenerateGalaxyMethod
        {
            /// <summary>
            /// Test to make sure that the first Galaxy Settings will be grabbed when there are no existing ones.
            /// </summary>
            [Test]
            public void RepositoryHasGalaxySettings()
            {
                var galaxyRepository = new Mock<IGalaxyRepository>();
                galaxyRepository.Setup(gr => gr.Add(It.IsAny<Galaxy>())).Returns<Galaxy>(g => g);

                var galaxySettingsRepository = new Mock<IGalaxySettingsRepository>();
                galaxySettingsRepository.Setup(gsr => gsr.EagerAll).Returns(new List<GalaxySettings>
                    {
                        new GalaxySettings()
                    }.AsQueryable);

                var game = new Game(null, galaxyRepository.Object, galaxySettingsRepository.Object, null, null, null);

                var galaxy = game.GenerateGalaxy(null);

                Assert.AreNotEqual(galaxy, null);

                galaxySettingsRepository.Verify(g => g.EagerAll, Times.Once());
                galaxySettingsRepository.Verify(g => g.EagerGet(It.IsAny<int>()), Times.Never());
                galaxyRepository.Verify(g => g.Add(It.IsAny<Galaxy>()), Times.Once());
            }

            /// <summary>
            /// Test to make sure that we can get galaxy settings based on an ID.
            /// </summary>
            [Test]
            public void CanGetGalaxySettingsFromRepository()
            {
                var galaxyRepository = new Mock<IGalaxyRepository>();
                galaxyRepository.Setup(gr => gr.Add(It.IsAny<Galaxy>())).Returns<Galaxy>(g => g);

                var galaxySettingsRepository = new Mock<IGalaxySettingsRepository>();
                galaxySettingsRepository.Setup(gsr => gsr.EagerGet(It.IsAny<int>())).Returns(new GalaxySettings());

                var game = new Game(null, galaxyRepository.Object, galaxySettingsRepository.Object, null, null, null);

                var galaxy = game.GenerateGalaxy(0);

                Assert.AreNotEqual(galaxy, null);

                galaxySettingsRepository.Verify(g => g.EagerAll, Times.Never());
                galaxySettingsRepository.Verify(g => g.EagerGet(It.IsAny<int>()), Times.Once());
                galaxyRepository.Verify(g => g.Add(It.IsAny<Galaxy>()), Times.Once());
            }

            /// <summary>
            /// Test to make sure that the first Galaxy Settings will be grabbed when there are no existing ones.
            /// </summary>
            [Test]
            public void RepositoryDoesNotHaveGalaxySettings()
            {
                var galaxyRepository = new Mock<IGalaxyRepository>();

                var galaxySettingsRepository = new Mock<IGalaxySettingsRepository>();
                galaxySettingsRepository.Setup(gsr => gsr.EagerAll).Returns(new List<GalaxySettings>().AsQueryable);

                var game = new Game(null, galaxyRepository.Object, galaxySettingsRepository.Object, null, null, null);

                var galaxy = game.GenerateGalaxy(null);

                Assert.AreEqual(galaxy, null);

                galaxySettingsRepository.Verify(g => g.EagerAll, Times.Once());
                galaxySettingsRepository.Verify(g => g.EagerGet(It.IsAny<int>()), Times.Never());
                galaxyRepository.Verify(g => g.Add(It.IsAny<Galaxy>()), Times.Never());
            }

            /// <summary>
            /// Test to make sure that if we can't get galaxy settings, the method returns null.
            /// </summary>
            [Test]
            public void CannotGetGalaxySettingsFromRepository()
            {
                var galaxyRepository = new Mock<IGalaxyRepository>();

                var galaxySettingsRepository = new Mock<IGalaxySettingsRepository>();
                galaxySettingsRepository.Setup(gsr => gsr.EagerGet(It.IsAny<int>()));

                var game = new Game(null, galaxyRepository.Object, galaxySettingsRepository.Object, null, null, null);

                var galaxy = game.GenerateGalaxy(0);

                Assert.AreEqual(galaxy, null);

                galaxySettingsRepository.Verify(g => g.EagerAll, Times.Never());
                galaxySettingsRepository.Verify(g => g.EagerGet(It.IsAny<int>()), Times.Once());
                galaxyRepository.Verify(g => g.Add(It.IsAny<Galaxy>()), Times.Never());
            }

            /// <summary>
            /// Test to make sure that the first Galaxy Settings will be grabbed when there are no existing ones.
            /// </summary>
            [Test]
            public void WillCreateOneSolarSystemInGalaxy()
            {
                var galaxyRepository = new Mock<IGalaxyRepository>();
                galaxyRepository.Setup(gsr => gsr.Add(It.IsAny<Galaxy>())).Returns<Galaxy>(g => g);

                var galaxySettingsRepository = new Mock<IGalaxySettingsRepository>();
                galaxySettingsRepository.Setup(gsr => gsr.EagerAll).Returns(new List<GalaxySettings>
                    {
                        new GalaxySettings
                            {
                                Width = 1,
                                Height = 1,
                                SolarSystemConstants = new SolarSystemConstants
                                    {
                                        SpatialEntityProbabilities = new List<SpatialEntityProbabilities>()
                                    }
                            }
                    }.AsQueryable);

                var solarSystemRepository = new Mock<ISolarSystemRepository>();
                solarSystemRepository.Setup(ssr => ssr.Create()).Returns(new SolarSystem());

                var game = new Game(null, galaxyRepository.Object, galaxySettingsRepository.Object, solarSystemRepository.Object, null, null);

                var galaxy = game.GenerateGalaxy(null);

                Assert.AreNotEqual(galaxy, null);
                Assert.AreEqual(galaxy.SolarSystems.Count, 1);

                galaxySettingsRepository.Verify(g => g.EagerAll, Times.Once());
                galaxySettingsRepository.Verify(g => g.EagerGet(It.IsAny<int>()), Times.Never());

                solarSystemRepository.Verify(ssr => ssr.Create(), Times.Once());

                galaxyRepository.Verify(g => g.Add(It.IsAny<Galaxy>()), Times.Once());
            }

            /// <summary>
            /// Test to make sure that the first Galaxy Settings will be grabbed when there are no existing ones.
            /// </summary>
            [Test]
            public void WillCreateOneSolarSystemInGalaxyWithAPlanet()
            {
                var galaxyRepository = new Mock<IGalaxyRepository>();
                galaxyRepository.Setup(gsr => gsr.Add(It.IsAny<Galaxy>())).Returns<Galaxy>(g => g);

                var galaxySettingsRepository = new Mock<IGalaxySettingsRepository>();
                galaxySettingsRepository.Setup(gsr => gsr.EagerAll).Returns(new List<GalaxySettings>
                    {
                        new GalaxySettings
                            {
                                Width = 1,
                                Height = 1,
                                SolarSystemConstants = new SolarSystemConstants
                                    {
                                        MinimumEntities = 1,
                                        MaximumEntities = 1,
                                        SpatialEntityProbabilities = new List<SpatialEntityProbabilities>
                                            {
                                                new SpatialEntityProbabilities
                                                    {
                                                        Type = SpatialEntityType.Planet,
                                                        SpawningProbability = 1
                                                    }
                                            }
                                    }
                            }
                    }.AsQueryable);

                var solarSystemRepository = new Mock<ISolarSystemRepository>();
                solarSystemRepository.Setup(ssr => ssr.Create()).Returns(new SolarSystem());

                var planetRepository = new Mock<IPlanetRepository>();
                planetRepository.Setup(pr => pr.Create()).Returns(new Planet());

                var spatialEntityRepository = new Mock<ISpatialEntityRepository>();

                var game = new Game(null, galaxyRepository.Object, galaxySettingsRepository.Object, solarSystemRepository.Object, planetRepository.Object, spatialEntityRepository.Object);

                var galaxy = game.GenerateGalaxy(null);

                Assert.AreNotEqual(galaxy, null);
                Assert.AreEqual(galaxy.SolarSystems.Count, 1);
                Assert.AreEqual(galaxy.SolarSystems.ElementAt(0).SpatialEntities.Count, 1);

                galaxySettingsRepository.Verify(g => g.EagerAll, Times.Once());
                galaxySettingsRepository.Verify(g => g.EagerGet(It.IsAny<int>()), Times.Never());

                solarSystemRepository.Verify(ssr => ssr.Create(), Times.Once());

                galaxyRepository.Verify(g => g.Add(It.IsAny<Galaxy>()), Times.Once());
            }
        }

        /// <summary>
        /// Test method for creating 
        /// </summary>
        [TestFixture]
        public class TheCreateSolarSystemMethod
        {
            /// <summary>
            /// Test to make sure that we can create a solar system without any entities.
            /// </summary>
            [Test]
            public void WillCreateSolarSystemWithoutEntities()
            {
                var galaxySettings = new GalaxySettings
                    {
                        SolarSystemConstants = new SolarSystemConstants
                            {
                                SpatialEntityProbabilities = new Collection<SpatialEntityProbabilities>()
                            }
                    };

                var solarSystemRepository = new Mock<ISolarSystemRepository>();
                solarSystemRepository.Setup(ssr => ssr.Create()).Returns(new SolarSystem());

                var game = new Game(null, null, null, solarSystemRepository.Object, null, null);
                var solarSystem = game.CreateSolarSystem(galaxySettings);

                Assert.IsNotNull(solarSystem);
                Assert.AreEqual(solarSystem.SpatialEntities.Count, 0);
            }

            /// <summary>
            /// Test to make sure that an entity can be created for a solar system.
            /// </summary>
            [Test]
            public void WillCreateSolarSystemWithOneEntity()
            {
                var galaxySettings = new GalaxySettings
                {
                    Width = 1,
                    Height = 1,
                    SolarSystemConstants = new SolarSystemConstants
                    {
                        MaximumEntities = 1,
                        MinimumEntities = 1,
                        SpatialEntityProbabilities = new Collection<SpatialEntityProbabilities>
                            {
                                new SpatialEntityProbabilities
                                    {
                                        Type = SpatialEntityType.Star,
                                        SpawningProbability = 1
                                    }
                            }
                    }
                };

                var solarSystemRepository = new Mock<ISolarSystemRepository>();
                solarSystemRepository.Setup(ssr => ssr.Create()).Returns(new SolarSystem());

                var spatialEntityRepository = new Mock<ISpatialEntityRepository>();
                spatialEntityRepository.Setup(pr => pr.Create()).Returns(new Planet());

                var game = new Game(null, null, null, solarSystemRepository.Object, null, spatialEntityRepository.Object);
                var solarSystem = game.CreateSolarSystem(galaxySettings);

                Assert.IsNotNull(solarSystem);
                Assert.AreEqual(solarSystem.SpatialEntities.Count, 1);
                Assert.AreEqual(solarSystem.Planets.Count, 0);
            }

            /// <summary>
            /// Test to make sure that a planet can be created for a solar system.
            /// </summary>
            [Test]
            public void WillCreateSolarSystemWithOnePlanet()
            {
                var galaxySettings = new GalaxySettings
                {
                    Width = 1,
                    Height = 1,
                    SolarSystemConstants = new SolarSystemConstants
                    {
                        MaximumEntities = 1,
                        MinimumEntities = 1,
                        SpatialEntityProbabilities = new Collection<SpatialEntityProbabilities>
                            {
                                new SpatialEntityProbabilities
                                    {
                                        Type = SpatialEntityType.Planet,
                                        SpawningProbability = 1
                                    }
                            }
                    }
                };

                var solarSystemRepository = new Mock<ISolarSystemRepository>();
                solarSystemRepository.Setup(ssr => ssr.Create()).Returns(new SolarSystem());

                var planetRepository = new Mock<IPlanetRepository>();
                planetRepository.Setup(pr => pr.Create()).Returns(new Planet());

                var game = new Game(null, null, null, solarSystemRepository.Object, planetRepository.Object, null);
                var solarSystem = game.CreateSolarSystem(galaxySettings);

                Assert.IsNotNull(solarSystem);
                Assert.AreEqual(solarSystem.SpatialEntities.Count, 1);
                Assert.AreEqual(solarSystem.Planets.Count, 1);
            }
        }
    }
}
