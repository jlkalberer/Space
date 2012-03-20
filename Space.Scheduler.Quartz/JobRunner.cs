// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobRunner.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   This is used to run jobs of specified types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System.Reflection;

    using Ninject;

    using global::Quartz;

    using Space.Scheduler.Jobs;

    /// <summary>
    /// This is used to run jobs of specified types.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of object to run the job on.
    /// </typeparam>
    public class JobRunner<TType> : IJob where TType : IGameJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRunner{TType}"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public JobRunner(IKernel kernel)
        {
            this.Job = kernel.Get<TType>();
        }

        /// <summary>
        /// Gets Job.
        /// </summary>
        public TType Job { get; private set; }

        #region Implementation of IJob

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler"/> when a <see cref="T:Quartz.ITrigger"/>
        ///             fires that is associated with the <see cref="T:Quartz.IJob"/>.
        /// </summary>
        /// <remarks>
        /// The implementation may wish to set a  result object on the 
        ///             JobExecutionContext before this method exits.  The result itself
        ///             is meaningless to Quartz, but may be informative to 
        ///             <see cref="T:Quartz.IJobListener"/>s or 
        ///             <see cref="T:Quartz.ITriggerListener"/>s that are watching the job's 
        ///             execution.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        public void Execute(IJobExecutionContext context)
        {
            var data = context.JobDetail.JobDataMap;

            // iterate over the job and populate the 
            var properties = typeof(TType).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (PropertyInfo pi in properties)
            {
                pi.SetValue(this.Job, data[pi.Name], null);
            }

            Job.Run();
        }

        #endregion
    }
}
