namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementTypeEntity : EntityBase, IColorSchemeEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string IntegrationUnit { get; set; } = string.Empty;

        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;

        public int MeterId { get; set; }
        public virtual MeterEntity? Meter { get; set; }
    }
}
