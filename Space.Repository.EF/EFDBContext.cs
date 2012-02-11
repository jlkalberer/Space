using System.Data.Entity;
using Space.DTO;
using Space.DTO.Spatial;
using Space.Repository.EF.Entities;

namespace Space.Repository.EF
{
    public class EFDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<ResearchPoints> ResearchPoints { get; set; }
        public DbSet<NetValue> PlayerNetValues { get; set; }
        public DbSet<Race> Races { get; set; }

        public DbSet<Galaxy> Galaxies { get; set; }
        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<SpatialEntity> SpatialEntities { get; set; }

        public DbSet<GalaxySettings> GalaxySettings { get; set; }
        public DbSet<Constant> Constants { get; set; }


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
                .HasKey(n => n.PlayerID);

            modelBuilder.Entity<Player>()
                .HasOptional(p => p.TotalNetValue)
                .WithRequired();

            modelBuilder.Entity<Fleet>()
                .HasKey(n => n.FleetID );

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
                .WithRequiredDependent();

            modelBuilder.Entity<SolarSystem>()
                .HasKey(ss => ss.ID);

            modelBuilder.Entity<SolarSystem>()
                .HasMany(s => s.Planets)
                .WithRequired();

            modelBuilder.Entity<SolarSystem>()
                .HasMany(s => s.SpatialEntities)
                .WithRequired()
                .HasForeignKey(s => s.SolarSystemID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GalaxySettings>()
                .HasKey(gs => gs.GalaxyID);
        }
    }
}
