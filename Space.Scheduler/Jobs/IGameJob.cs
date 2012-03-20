// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameJob.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The interface shared by all jobs in the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Scheduler.Jobs
{
    /// <summary>
    /// The interface shared by all jobs in the game.
    /// </summary>
    public interface IGameJob
    {
        /// <summary>
        /// Runs code specific to the game.
        /// </summary>
        void Run();
    }
}
