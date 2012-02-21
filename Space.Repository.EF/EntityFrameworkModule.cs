// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityFrameworkModule.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   A NinjectModule used for dependency injection on interfaces implementations using the EntityFramework.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System.Data.Entity;

    using Ninject.Modules;

    /// <summary>
    /// A NinjectModule used for dependency injection on interfaces implementations using the EntityFramework.
    /// </summary>
    public class EntityFrameworkModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Map all interfaces to implementations.
        /// </summary>
        public override void Load()
        {
            Bind<EntityFrameworkDbContext>().ToSelf().InSingletonScope();
            Bind<IDatabaseInitializer<EntityFrameworkDbContext>>().To<EntityFrameworkInitializer>().InSingletonScope();
            Bind<IPlayerRepository>().To<PlayerRepository>();

            Bind<IGalaxyRepository>().To<GalaxyRepository>();
            Bind<IGalaxySettingsRepository>().To<GalaxySettingsRepository>();
            Bind<ISolarSystemRepository>().To<SolarSystemRepository>();
            Bind<ISpatialEntityRepository>().To<SpatialEntityRepository>();
            Bind<IPlanetRepository>().To<PlanetRepository>();
        }

        #endregion
    }
}
