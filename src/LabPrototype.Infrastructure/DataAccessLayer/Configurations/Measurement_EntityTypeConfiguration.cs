using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class Measurement_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.MeasurementType)
                .WithMany(x => x.Measurements);

            builder
                .HasOne(x => x.MeasurementGroup)
                .WithMany(x => x.Measurements);
        }
    }
}
