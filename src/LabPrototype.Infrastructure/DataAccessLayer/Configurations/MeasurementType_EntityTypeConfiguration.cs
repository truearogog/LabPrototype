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
                .HasOne(x => x.ColorScheme)
                .WithMany(x => x.MeasurementTypes);

            builder
                .HasMany(x => x.MeasurementGroupSchemaMeasurementTypes)
                .WithOne(x => x.MeasurementType);

            builder
                .HasData(new MeasurementTypeEntity[]
                {
                    new MeasurementTypeEntity { Id = 1, Name = "Q1",    Unit = "m³/h",  ColorSchemeId = 1 },
                    new MeasurementTypeEntity { Id = 2, Name = "Q2",    Unit = "m³/h",  ColorSchemeId = 2 },
                    new MeasurementTypeEntity { Id = 3, Name = "ΔQ",    Unit = "m³/h",  ColorSchemeId = 3 },
                    new MeasurementTypeEntity { Id = 4, Name = "P",     Unit = "MW",    ColorSchemeId = 4 },
                    new MeasurementTypeEntity { Id = 5, Name = "T",     Unit = "°C",    ColorSchemeId = 5 },
                });
        }
    }
}
