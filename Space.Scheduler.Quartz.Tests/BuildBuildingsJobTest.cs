// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildBuildingsJobTest.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Tests for the BuildBuildingsJob.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz.Tests
{
    using System.Collections.Generic;

    using Moq;

    using NUnit.Framework;

    using Space.DTO;
    using Space.DTO.Buildings;

    using global::Quartz;

    using Space.Repository;
    using Space.Scheduler.Quartz.Jobs;

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

                var context = new Mock<IJobExecutionContext>();
                var jobDetail = new Mock<IJobDetail>();
                jobDetail.Setup(jd => jd.JobDataMap).Returns(new JobDataMap());
                context.Setup(ctx => ctx.JobDetail).Returns(jobDetail.Object);

                job.Execute(context.Object);

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
                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object);

                var context = new Mock<IJobExecutionContext>();
                var jobDetail = new Mock<IJobDetail>();
                jobDetail.Setup(jd => jd.JobDataMap).Returns(new JobDataMap()
                    {
                        new KeyValuePair<string, object>("PlanetID", 1)
                    });
                context.Setup(ctx => ctx.JobDetail).Returns(jobDetail.Object);

                job.Execute(context.Object);

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Never());
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Never());
            }

            /// <summary>
            /// Test that the function returns when the planet ID is not passed in.
            /// </summary>
            [Test]
            public void CostDoesntExist()
            {
                var planetRepository = new Mock<IPlanetRepository>();
                var playerRepository = new Mock<IPlayerRepository>();
                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object);

                var context = new Mock<IJobExecutionContext>();
                var jobDetail = new Mock<IJobDetail>();
                jobDetail.Setup(jd => jd.JobDataMap).Returns(new JobDataMap()
                    {
                        new KeyValuePair<string, object>("PlanetID", 1),
                        new KeyValuePair<string, object>("PlayerID", 1)
                    });
                context.Setup(ctx => ctx.JobDetail).Returns(jobDetail.Object);

                job.Execute(context.Object);

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                jobDetail.Verify(jd => jd.JobDataMap, Times.Exactly(3));
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Never());
            }
            
            /// <summary>
            /// Test that the function returns when the planet ID is not passed in.
            /// </summary>
            [Test]
            public void BuildingTypeDoesntExist()
            {
                var planetRepository = new Mock<IPlanetRepository>();
                var playerRepository = new Mock<IPlayerRepository>();
                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object);

                var context = new Mock<IJobExecutionContext>();
                var jobDetail = new Mock<IJobDetail>();
                jobDetail.Setup(jd => jd.JobDataMap).Returns(new JobDataMap()
                    {
                        new KeyValuePair<string, object>("PlanetID", 1),
                        new KeyValuePair<string, object>("PlayerID", 1),
                        new KeyValuePair<string, object>("Costs", new NetValue())
                    });
                context.Setup(ctx => ctx.JobDetail).Returns(jobDetail.Object);

                job.Execute(context.Object);

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                jobDetail.Verify(jd => jd.JobDataMap, Times.Exactly(4));
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
                var job = new BuildBuildingsJob(planetRepository.Object, playerRepository.Object);

                var context = new Mock<IJobExecutionContext>();
                var jobDetail = new Mock<IJobDetail>();
                jobDetail.Setup(jd => jd.JobDataMap).Returns(new JobDataMap()
                    {
                        new KeyValuePair<string, object>("PlanetID", 1),
                        new KeyValuePair<string, object>("PlayerID", 1),
                        new KeyValuePair<string, object>("Costs", new NetValue()),
                        new KeyValuePair<string, object>("BuildingType", BuildingType.CashFactory)
                    });
                context.Setup(ctx => ctx.JobDetail).Returns(jobDetail.Object);

                job.Execute(context.Object);

                planetRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                playerRepository.Verify(pr => pr.Get(It.IsAny<int>()), Times.Once());
                jobDetail.Verify(jd => jd.JobDataMap, Times.Exactly(4));
                planetRepository.Verify(pr => pr.SaveChanges(), Times.Once());
            }
        }
    }
}
