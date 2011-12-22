using System.Data.Entity;
using Ninject.Modules;

namespace Space.Repository.EF
{
    public class EFModule : NinjectModule
    {
        #region Overrides of NinjectModule

        public override void Load()
        {
            Bind<DbContext>().To<EFDBContext>().InSingletonScope();
            Bind<IPlayerRepository>().To<PlayerRepository>();
            Bind<ISolarSystemRepository>().To<SolarSystemRepository>();
            Bind<IPlanetRepository>().To<PlanetRepository>();
        }

        #endregion
    }
}
