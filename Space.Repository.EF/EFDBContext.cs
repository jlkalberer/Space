using System.Data.Entity;
using Space.DTO;

namespace Space.Repository.EF
{
    public class EFDBContext : DbContext
    {
        public EFDBContext()
        {
            var bleh = 1;
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<ResearchPoints> ResearchPoints { get; set; }
        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<NetValue> PlayerNetValues { get; set; }
        public DbSet<Race> Races { get; set; }

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
                .HasKey(p => p.ID);

            modelBuilder.Entity<Planet>()
                .HasOptional(p => p.Owner)
                .WithMany(p => p.Planets);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Planets)
                .WithOptional(p => p.Owner);

            modelBuilder.Entity<SolarSystem>()
                .HasMany(s => s.Planets)
                .WithRequired()
                .HasForeignKey(p => p.SolarSystemID);

            modelBuilder.Entity<SolarSystem>()
                .HasRequired(s => s.SpatialEntities);

            modelBuilder.Entity<SpatialEntity>()
                .HasKey(s => s.ID);
        }
    }
}
