using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class Meter_EntityTypeConfiguration : EntityTypeConfigurationBase<MeterEntity>
    {
        public override void Configure(EntityTypeBuilder<MeterEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasMany(x => x.MeasurementTypes)
                .WithOne(x => x.Meter);

            builder
                .HasMany(x => x.Archives)
                .WithOne(x => x.Meter);
        }
    }
}
