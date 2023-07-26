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
                .Property(x => x.AverageValues)
                .HasConversion(
                    v => v.SelectMany(value => BitConverter.GetBytes(value)).ToArray(),
                    v => Enumerable.Range(0, v.Length / sizeof(double))
                            .Select(offset => BitConverter.ToDouble(v, offset * sizeof(double)))
                            .ToArray());

            builder
                .Property(x => x.SummaryValues)
                .HasConversion(
                    v => v.SelectMany(value => BitConverter.GetBytes(value)).ToArray(),
                    v => Enumerable.Range(0, v.Length / sizeof(double))
                            .Select(offset => BitConverter.ToDouble(v, offset * sizeof(double)))
                            .ToArray());

            builder
                .HasOne(x => x.MeasurementGroupArchive)
                .WithMany(x => x.MeasurementGroups);

            builder
                .HasOne(x => x.MeasurementGroupSchema)
                .WithMany(x => x.MeasurementGroups);
        }
    }
}
