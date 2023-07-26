using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeasurementGroupArchive_EntityTypeConfiguration : EntityTypeConfigurationBase<MeasurementGroupArchiveEntity>
    {
        public override void Configure(EntityTypeBuilder<MeasurementGroupArchiveEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Meter)
                .WithMany(x => x.MeasurementGroupArchives);

            builder
                .HasMany(x => x.MeasurementGroups)
                .WithOne(x => x.MeasurementGroupArchive);
        }
    }
}
