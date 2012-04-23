// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuartzModule.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Ninject module for setting up Quartz
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System;

    using Ninject;
    using Ninject.Modules;

    using global::Quartz;
    using global::Quartz.Spi;

    /// <summary>
    /// Ninject module for setting up Quartz
    /// </summary>
    public class QuartzModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IJobFactory>().To<NinjectJobFactory>();
            Bind<ISchedulerFactory>().ToProvider<QuartzSchedulerFactoryProvider>();
            Bind<IScheduler>().ToProvider<QuartzSchedulerProvider>().InSingletonScope();
            Bind<Func<Type, IJob>>().ToMethod(ctx => t => (IJob)ctx.Kernel.Get(t));

            Bind<ISpaceScheduler>().To<SpaceScheduler>();
        }

        #endregion
    }
}
