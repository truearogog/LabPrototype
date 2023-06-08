using LabPrototype.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeasurementGroup_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementGroupEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementGroupEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Meter)
                .WithMany(x => x.MeasurementGroups);

            builder
                .HasMany(x => x.Measurements)
                .WithOne(x => x.MeasurementGroup);
        }
    }
}
