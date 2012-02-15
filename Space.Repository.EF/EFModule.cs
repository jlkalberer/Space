using Ninject.Modules;

namespace Space.Repository.EF
{
    public class EFModule : NinjectModule
    {
        #region Overrides of NinjectModule

        public override void Load()
        {
            Bind<EFDBContext>().ToSelf().InSingletonScope();
            Bind<IPlayerRepository>().To<PlayerRepository>();

            Bind<IGalaxyRepository>().To<GalaxyRepository>();
            Bind<ISolarSystemRepository>().To<SolarSystemRepository>();
            Bind<IEntityRepository>().To<EntityRepository>();
            Bind<IPlanetRepository>().To<PlanetRepository>();
            //Bind<IConstantsProvider>().To<ConstantsProvider>();
        }

        #endregion
    }
}
