namespace LabPrototype.Domain.Entities
{
    public class MeterTypeEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int ColorSchemeId { get; set; }
        public virtual ColorSchemeEntity? ColorScheme { get; set; }

        public virtual ICollection<MeterTypeMeasurementTypeEntity> MeterTypeMeasurementTypes { get; set; } = new List<MeterTypeMeasurementTypeEntity>();
        public virtual ICollection<MeterEntity> Meters { get; set; } = new List<MeterEntity>();
    }
}
