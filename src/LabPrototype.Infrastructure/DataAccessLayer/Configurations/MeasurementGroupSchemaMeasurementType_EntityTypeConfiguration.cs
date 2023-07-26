using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeasurementGroupSchemaMeasurementType_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementGroupSchemaMeasurementTypeEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementGroupSchemaMeasurementTypeEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.MeasurementGroupSchema)
                .WithMany(x => x.MeasurementGroupSchemaMeasurementTypes);

            builder
                .HasOne(x => x.MeasurementType)
                .WithMany(x => x.MeasurementGroupSchemaMeasurementTypes);

            builder
                .HasData(new[]
                {
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 1, MeasurementGroupSchemaId = 1, MeasurementTypeId = 1 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 2, MeasurementGroupSchemaId = 1, MeasurementTypeId = 2 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 3, MeasurementGroupSchemaId = 1, MeasurementTypeId = 3 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 4, MeasurementGroupSchemaId = 2, MeasurementTypeId = 1 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 5, MeasurementGroupSchemaId = 2, MeasurementTypeId = 2 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 6, MeasurementGroupSchemaId = 2, MeasurementTypeId = 3 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 7, MeasurementGroupSchemaId = 2, MeasurementTypeId = 4 },
                    new MeasurementGroupSchemaMeasurementTypeEntity { Id = 8, MeasurementGroupSchemaId = 2, MeasurementTypeId = 5 },
                });
        }
    }
}
