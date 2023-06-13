namespace LabPrototype.Domain.Entities
{
    public class MeasurementTypeEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public int ColorSchemeId { get; set; }
        public virtual ColorSchemeEntity? ColorScheme { get; set; }

        public virtual ICollection<MeterTypeMeasurementTypeEntity> MeterTypeMeasurementTypes { get; set; } = new List<MeterTypeMeasurementTypeEntity>();
        public virtual ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    }
}
