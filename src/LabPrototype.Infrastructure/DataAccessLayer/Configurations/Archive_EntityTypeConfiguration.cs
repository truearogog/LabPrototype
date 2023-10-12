using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class Archive_EntityTypeConfiguration : EntityTypeConfigurationBase<ArchiveEntity>
    {
        public override void Configure(EntityTypeBuilder<ArchiveEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Meter)
                .WithMany(x => x.Archives);

            builder
                .HasMany(x => x.MeasurementGroups)
                .WithOne(x => x.MeasurementGroupArchive);
        }
    }
}
