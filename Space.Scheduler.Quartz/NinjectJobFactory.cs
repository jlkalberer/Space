// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectJobFactory.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A job factory that uses Ninject.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System;

    using global::Quartz;
    using global::Quartz.Spi;

    /// <summary>
    /// A job factory that uses Ninject.
    /// </summary>
    public class NinjectJobFactory : IJobFactory
    {
        /// <summary>
        /// The member to hold the job factory.
        /// </summary>
        private readonly Func<Type, IJob> jobFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectJobFactory"/> class.
        /// </summary>
        /// <param name="jobFactory">
        /// The job factory.
        /// </param>
        public NinjectJobFactory(Func<Type, IJob> jobFactory)
        {
            this.jobFactory = jobFactory;
        }

        /// <summary>
        /// Create a new Job.
        /// </summary>
        /// <param name="bundle">
        /// The bundle.
        /// </param>
        /// <param name="scheduler">
        /// Reference to the current scheduler.
        /// </param>
        /// <returns>
        /// The requested job.
        /// </returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return this.jobFactory(bundle.JobDetail.JobType);
        }
    }
}
