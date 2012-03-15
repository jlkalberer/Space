// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuartzSchedulerFactoryProvider.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Setup for Quartz.  Determines how the tasks will be stored.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System.Collections.Specialized;

    using Ninject.Activation;

    using global::Quartz;
    using global::Quartz.Impl;

    /// <summary>
    /// Setup for Quartz.  Determines how the tasks will be stored.
    /// </summary>
    public class QuartzSchedulerFactoryProvider : Provider<ISchedulerFactory>
    {
        /// <summary>
        /// Creates an instance of a Scheduler Factory.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The requested Scheduler Factory.
        /// </returns>
        protected override ISchedulerFactory CreateInstance(IContext context)
        {
            return new StdSchedulerFactory();

            // var properties = new NameValueCollection();
            // properties["quartz.dataSource.DataSource.connectionString"] = "Your connection string";
            // properties["quartz.dataSource.DataSource.provider"] = "Your provider";

            // properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            // properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz ";
            // properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            // properties["quartz.jobStore.dataSource"] = "DataSource";
            // properties["quartz.jobStore.useProperties"] = "true";

            // return new StdSchedulerFactory(properties);
        }
    }
}
