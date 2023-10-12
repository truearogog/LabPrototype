using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer
{
    public class LabDbContext : DbContext
    {
        public LabDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MeterEntity> Meters { get; set; }
        public DbSet<MeasurementTypeEntity> MeasurementTypes { get; set; }
        public DbSet<MeasurementGroupEntity> MeasurementGroups { get; set; }
        public DbSet<ArchiveEntity> Archives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
