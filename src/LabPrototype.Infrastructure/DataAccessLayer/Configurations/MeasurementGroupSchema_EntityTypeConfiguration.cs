using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeasurementGroupSchema_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementGroupSchemaEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementGroupSchemaEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.MeterType)
                .WithMany(x => x.MeasurementGroupSchemas);

            builder
                .HasMany(x => x.MeasurementGroups)
                .WithOne(x => x.MeasurementGroupSchema);

            builder
                .HasMany(x => x.MeasurementGroupSchemaMeasurementTypes)
                .WithOne(x => x.MeasurementGroupSchema);

            builder
                .HasData(new[]
                {
                    new MeasurementGroupSchemaEntity { Id = 1, MeterTypeId = 1, },
                    new MeasurementGroupSchemaEntity { Id = 2, MeterTypeId = 2, },
                });
        }
    }
}
