using LabPrototype.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeterTypeMeasurementType_EntityTypeConfiguration : EntityTypeConfigurationBase<MeterTypeMeasurementTypeEntity>
    {
        public override void Configure(EntityTypeBuilder<MeterTypeMeasurementTypeEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasData(new MeterTypeMeasurementTypeEntity[]
                {
                    new MeterTypeMeasurementTypeEntity { Id = 1, MeterTypeId = 1, MeasurementTypeId = 1 },
                    new MeterTypeMeasurementTypeEntity { Id = 2, MeterTypeId = 1, MeasurementTypeId = 2 },
                    new MeterTypeMeasurementTypeEntity { Id = 3, MeterTypeId = 1, MeasurementTypeId = 3 },
                    new MeterTypeMeasurementTypeEntity { Id = 4, MeterTypeId = 2, MeasurementTypeId = 1 },
                    new MeterTypeMeasurementTypeEntity { Id = 5, MeterTypeId = 2, MeasurementTypeId = 2 },
                    new MeterTypeMeasurementTypeEntity { Id = 6, MeterTypeId = 2, MeasurementTypeId = 3 },
                    new MeterTypeMeasurementTypeEntity { Id = 7, MeterTypeId = 2, MeasurementTypeId = 4 },
                    new MeterTypeMeasurementTypeEntity { Id = 8, MeterTypeId = 2, MeasurementTypeId = 5 },
                });
        }
    }
}
