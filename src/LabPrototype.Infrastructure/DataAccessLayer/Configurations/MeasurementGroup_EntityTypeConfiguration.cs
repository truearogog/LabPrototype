using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeasurementGroup_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementGroupEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementGroupEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.MeasurementGroupArchive)
                .WithMany(x => x.MeasurementGroups);

            builder
                .HasMany(x => x.Measurements)
                .WithOne(x => x.MeasurementGroup);
        }
    }
}
