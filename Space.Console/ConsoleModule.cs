using Ninject.Modules;
using Space.Repository.Entities;

namespace Space.Console
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConsoleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConstantsProvider>().To<ConstantsProvider>();
        }
    }
}
