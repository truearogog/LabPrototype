using LabPrototype.EntityFramework.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.EntityFramework
{
    public class LabDbContext : DbContext
    {
        public DbSet<MeterDto> Meters { get; set; }
        public DbSet<MeasurementDto> Measurements { get; set; }
        public DbSet<ApplicationDto> Application { get; set; }

        public LabDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeterDto>()
                .HasMany(meter => meter.Measurements)
                .WithOne(measurement => measurement.Meter)
                .IsRequired(false);
        }
    }
}
