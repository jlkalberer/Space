// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuartzSchedulerProvider.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Used to create instances of schedulers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System.Collections.Generic;

    using Ninject.Activation;

    using global::Quartz;
    using global::Quartz.Spi;

    /// <summary>
    /// Used to create instances of schedulers.
    /// </summary>
    public class QuartzSchedulerProvider : Provider<IScheduler>
    {
        /// <summary>
        /// Holds the job factory.
        /// </summary>
        private readonly IJobFactory jobFactory;

        /// <summary>
        /// Listeners for the scheduler.
        /// </summary>
        private readonly IEnumerable<ISchedulerListener> listeners;

        /// <summary>
        /// The factory for creating schedulers.
        /// </summary>
        private readonly ISchedulerFactory schedulerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuartzSchedulerProvider"/> class.
        /// </summary>
        /// <param name="schedulerFactory">
        /// The scheduler factory.
        /// </param>
        /// <param name="jobFactory">
        /// The job factory.
        /// </param>
        /// <param name="listeners">
        /// The listeners.
        /// </param>
        public QuartzSchedulerProvider(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<ISchedulerListener> listeners)
        {
            this.jobFactory = jobFactory;
            this.listeners = listeners;
            this.schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// Creates a new instance of a scheduler.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The requested scheduler.
        /// </returns>
        protected override IScheduler CreateInstance(IContext context)
        {
            var scheduler = this.schedulerFactory.GetScheduler();
            scheduler.JobFactory = this.jobFactory;
            foreach (var listener in this.listeners)
            {
                scheduler.ListenerManager.AddSchedulerListener(listener);
            }

            return scheduler;
        }
    }
}
