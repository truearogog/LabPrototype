namespace LabPrototype.Domain.Entities
{
    public class MeasurementTypeEntity : EntityBase
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }

        public int ColorSchemeId { get; set; }
        public virtual ColorSchemeEntity? ColorScheme { get; set; }

        public virtual ICollection<MeterTypeMeasurementTypeEntity>? MeterTypeMeasurementTypes { get; set; }
        public virtual ICollection<MeasurementEntity>? Measurements { get; set; }
    }
}
