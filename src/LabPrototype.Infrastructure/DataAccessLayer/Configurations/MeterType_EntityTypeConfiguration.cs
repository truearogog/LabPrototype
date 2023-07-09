using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class MeterType_EntityTypeConfiguration : EntityTypeConfigurationBase<MeterTypeEntity>
    {
        public override void Configure(EntityTypeBuilder<MeterTypeEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.ColorScheme)
                .WithMany(x => x.MeterTypes);

            builder
                .HasMany(x => x.MeterTypeMeasurementTypes)
                .WithOne(x => x.MeterType);

            builder
                .HasMany(x => x.Meters)
                .WithOne(x => x.MeterType);

            builder
                .HasData(new MeterTypeEntity[]
                {
                    new MeterTypeEntity { Id = 1, Name = "Meter one", Description = "Big meter!",       ColorSchemeId = 1 },
                    new MeterTypeEntity { Id = 2, Name = "Meter two", Description = "Small meter...",   ColorSchemeId = 2 },
                });
        }
    }
}
