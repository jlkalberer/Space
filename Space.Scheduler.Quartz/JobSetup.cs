// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobSetup.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   This is used to setup the job to run.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Quartz
{
    using System;
    using System.Linq.Expressions;

    using global::Quartz;

    using Space.Scheduler.Jobs;

    /// <summary>
    /// This is used to setup the job to run.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of job.
    /// </typeparam>
    public class JobSetup<TType> where TType : IGameJob
    {
        /// <summary>
        /// The scheduler used to setup the job.
        /// </summary>
        private readonly IScheduler scheduler;

        /// <summary>
        /// Holds the Job detail so we can set the properties.
        /// </summary>
        private readonly IJobDetail jobDetail;

        /// <summary>
        /// The JobKey used to set the JobDetail and Trigger.
        /// </summary>
        private readonly JobKey jobKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobSetup{TType}"/> class. 
        /// </summary>
        /// <param name="scheduler">
        /// The scheduler.
        /// </param>
        public JobSetup(IScheduler scheduler)
        {
            this.scheduler = scheduler;
            this.jobKey = new JobKey(Guid.NewGuid().ToString(), typeof(TType).ToString());
            this.jobDetail = JobBuilder
                                .Create<JobRunner<TType>>()
                                .WithIdentity(this.jobKey)
                                .Build();
        }

        /// <summary>
        /// Set a value of the job.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="TPropertyType">
        /// The type of the member that is to be set.
        /// </typeparam>
        public void Set<TPropertyType>(Expression<Func<TType, TPropertyType>> expression, TPropertyType value)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Could not convert the expression to a member expression.");
            }

            this.jobDetail.JobDataMap[memberExpression.Member.Name] = value;
        }

        /// <summary>
        /// Gets a value using an expression.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TPropertyType">
        /// The type of object to return.
        /// </typeparam>
        /// <returns>
        /// The member.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throws an exception if the expression isn't a MemberExpression.
        /// </exception>
        public TPropertyType Get<TPropertyType>(Expression<Func<TType, TPropertyType>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Could not convert the expression to a member expression.");
            }

            return (TPropertyType)this.jobDetail.JobDataMap.Get(memberExpression.Member.Name);
        }

        /// <summary>
        /// Tells the job to run in a tick amount.
        /// </summary>
        /// <param name="time">
        /// The time in ticks.
        /// </param>
        public void Run(DateTimeOffset time)
        {
            var trigger = TriggerBuilder
                .Create()
                .WithIdentity(new TriggerKey(this.jobKey.Name, this.jobKey.Group))
                .StartAt(time)
                .Build();

            this.scheduler.ScheduleJob(this.jobDetail, trigger);
        }
    }
}
