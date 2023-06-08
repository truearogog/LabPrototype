namespace LabPrototype.Domain.Entities
{
    public class MeterTypeEntity : EntityBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int ColorSchemeId { get; set; }
        public virtual ColorSchemeEntity? ColorScheme { get; set; }

        public virtual ICollection<MeterTypeMeasurementTypeEntity>? MeterTypeMeasurementTypes { get; set; }
        public virtual ICollection<MeterEntity>? Meters { get; set; }
    }
}
