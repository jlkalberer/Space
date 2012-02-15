using Ninject.Modules;
using Space.DTO.Entities;

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
