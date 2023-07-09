using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public class ColorScheme_EntityTypeConfiguration : EntityTypeConfigurationBase<ColorSchemeEntity>
    {
        public override void Configure(EntityTypeBuilder<ColorSchemeEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasMany(x => x.MeasurementTypes)
                .WithOne(x => x.ColorScheme);

            builder
                .HasMany(x => x.MeterTypes)
                .WithOne(x => x.ColorScheme);

            builder
                .HasData(new ColorSchemeEntity[]
                {
                    new ColorSchemeEntity { Id = 1, Name = "Red",       PrimaryColor = "#c0392b", SecondaryColor = "#e74c3c" },
                    new ColorSchemeEntity { Id = 2, Name = "Blue",      PrimaryColor = "#2980b9", SecondaryColor = "#3498db" },
                    new ColorSchemeEntity { Id = 3, Name = "Yellow",    PrimaryColor = "#f39c12", SecondaryColor = "#f1c40f" },
                    new ColorSchemeEntity { Id = 4, Name = "Orange",    PrimaryColor = "#d35400", SecondaryColor = "#e67e22" },
                    new ColorSchemeEntity { Id = 5, Name = "Green",     PrimaryColor = "#27ae60", SecondaryColor = "#2ecc71" },
                    new ColorSchemeEntity { Id = 6, Name = "Purple",    PrimaryColor = "#8e44ad", SecondaryColor = "#9b59b6" },
                    new ColorSchemeEntity { Id = 7, Name = "Turquoise", PrimaryColor = "#16a085", SecondaryColor = "#1abc9c" },
                    new ColorSchemeEntity { Id = 8, Name = "Silver",    PrimaryColor = "#bdc3c7", SecondaryColor = "#ecf0f1" },
                    new ColorSchemeEntity { Id = 9, Name = "Midnight",  PrimaryColor = "#2c3e50", SecondaryColor = "#34495e" },
                });
        }
    }
}
