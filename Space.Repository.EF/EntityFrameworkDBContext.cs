// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityFrameworkDBContext.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The Entity framework DataContext for the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System.Data.Entity;

    using Space.DTO;
    using Space.DTO.Players;
    using Space.DTO.Spatial;
    using Space.Repository.EF.Entities;

    /// <summary>
    /// The Entity framework DataContext for the game.
    /// </summary>
    public class EntityFrameworkDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets Players.
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets Fleets.
        /// </summary>
        public DbSet<Fleet> Fleets { get; set; }

        /// <summary>
        /// Gets or sets ResearchPoints.
        /// </summary>
        public DbSet<ResearchPoints> ResearchPoints { get; set; }

        /// <summary>
        /// Gets or sets PlayerNetValues.
        /// </summary>
        public DbSet<NetValue> PlayerNetValues { get; set; }

        /// <summary>
        /// Gets or sets PlayerTickValues.
        /// </summary>
        public DbSet<TickValue> PlayerTickValues { get; set; }

        /// <summary>
        /// Gets or sets Races.
        /// </summary>
        public DbSet<Race> Races { get; set; }

        /// <summary>
        /// Gets or sets Galaxies.
        /// </summary>
        public DbSet<Galaxy> Galaxies { get; set; }

        /// <summary>
        /// Gets or sets SolarSystems.
        /// </summary>
        public DbSet<SolarSystem> SolarSystems { get; set; }

        /// <summary>
        /// Gets or sets SpatialEntities.
        /// </summary>
        public DbSet<SpatialEntity> SpatialEntities { get; set; }

        /// <summary>
        /// Gets or sets GalaxySettings.
        /// </summary>
        public DbSet<GalaxySettings> GalaxySettings { get; set; }

        /// <summary>
        /// Gets or sets SolarSystemConstants.
        /// </summary>
        public DbSet<SolarSystemConstants> SolarSystemConstants { get; set; }

        /// <summary>
        /// Gets or sets SpatialEntityProbabilities.
        /// </summary>
        public DbSet<SpatialEntityProbabilities> SpatialEntityProbabilities { get; set; }

        /// <summary>
        /// Gets or sets SolarSystemConstants.
        /// </summary>
        public DbSet<BuildingCosts> BuildCosts { get; set; }
        
        /// <summary>
        /// Gets or sets Constants.
        /// </summary>
        public DbSet<Constant> Constants { get; set; }

        /// <summary>
        /// Code first initialization for the EntityFrameworkDbContext
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .HasKey(p => p.ID);

            modelBuilder.Entity<ResearchPoints>()
                .HasKey(p => p.PlayerID);

            modelBuilder.Entity<Player>()
                .HasOptional(p => p.ResearchPoints)
                .WithRequired();

            modelBuilder.Entity<NetValue>()
                .HasKey(nv => nv.PlayerID);

            modelBuilder.Entity<Player>()
                .HasRequired(p => p.TotalNetValue)
                .WithRequiredPrincipal();

            modelBuilder.Entity<TickValue>()
                .HasKey(tv => tv.PlayerID);

            modelBuilder.Entity<Player>()
                .HasRequired(p => p.TickValue)
                .WithRequiredPrincipal();

            modelBuilder.Entity<Fleet>()
                .HasKey(n => n.FleetID);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Fleets)
                .WithRequired()
                .HasForeignKey(f => f.ID);

            modelBuilder.Entity<Race>()
                .HasKey(r => r.ID);

            modelBuilder.Entity<Player>()
                .HasOptional(p => p.Race)
                .WithOptionalPrincipal();

            modelBuilder.Entity<Planet>()
                .HasOptional(p => p.Owner)
                .WithMany(p => p.Planets);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Planets)
                .WithOptional(p => p.Owner);

            modelBuilder.Entity<Galaxy>()
                .HasKey(g => g.ID);

            modelBuilder.Entity<Galaxy>()
                .HasMany(p => p.Players)
                .WithRequired(g => g.Galaxy);

            modelBuilder.Entity<Galaxy>()
                .HasMany(g => g.SolarSystems)
                .WithRequired()
                .HasForeignKey(s => s.GalaxyID);

            modelBuilder.Entity<Galaxy>()
                .HasRequired(g => g.GalaxySettings)
                .WithOptional();

            modelBuilder.Entity<SolarSystem>()
                .HasKey(ss => ss.ID);

            modelBuilder.Entity<SolarSystem>()
                .HasMany(s => s.Planets)
                .WithRequired();

            modelBuilder.Entity<SolarSystem>()
                .HasMany(s => s.SpatialEntities)
                .WithRequired(s => s.SolarSystem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GalaxySettings>()
                .HasKey(gs => gs.ID);

            modelBuilder.Entity<GalaxySettings>()
                .HasRequired(gs => gs.SolarSystemConstants)
                .WithRequiredPrincipal();

            modelBuilder.Entity<GalaxySettings>()
                .HasMany(gs => gs.BuildingCosts)
                .WithRequired();

            modelBuilder.Entity<SolarSystemConstants>()
                .HasMany(ssc => ssc.SpatialEntityProbabilities)
                .WithRequired()
                .HasForeignKey(sep => sep.SolarSystemConstantsID);
        }
    }
}
