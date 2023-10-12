using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeasurementType_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementTypeEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementTypeEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Meter)
                .WithMany(x => x.MeasurementTypes);
        }
    }
}
