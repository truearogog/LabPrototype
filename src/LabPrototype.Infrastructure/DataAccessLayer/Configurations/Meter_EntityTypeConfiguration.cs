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
                .HasOne(x => x.MeterType)
                .WithMany(x => x.Meters);

            builder
                .HasMany(x => x.MeasurementGroups)
                .WithOne(x => x.Meter);
        }
    }
}
