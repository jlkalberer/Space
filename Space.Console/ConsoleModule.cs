// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleModule.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Console module for setting up Ninject.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Console
{
    using Ninject.Modules;

    using Space.DTO.Entities;
    using Space.Repository;

    /// <summary>
    /// Console module for setting up Ninject.
    /// </summary>
    public class ConsoleModule : NinjectModule
    {
        /// <summary>
        /// Binds interfaces to implementations.
        /// </summary>
        public override void Load()
        {
            Bind<IConstantsProvider>().To<ConstantsProvider>();
        }
    }
}
