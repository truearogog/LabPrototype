using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer
{
    public class LabDbContext : DbContext
    {
        public LabDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ColorSchemeEntity> ColorSchemes { get; set; }
        public DbSet<MeterEntity> Meters { get; set; }
        public DbSet<MeterTypeEntity> MeterTypes { get; set; }
        public DbSet<MeasurementTypeEntity> MeasurementTypes { get; set; }
        public DbSet<MeasurementGroupEntity> MeasurementGroups { get; set; }
        public DbSet<MeasurementGroupSchemaMeasurementTypeEntity> MeasurementGroupSchemeMeasurementTypes { get; set; }
        public DbSet<MeasurementGroupArchiveEntity> MeasurementGroupArchives { get; set; }
        public DbSet<MeasurementGroupSchemaEntity> MeasurementGroupSchemas { get; set; }

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
