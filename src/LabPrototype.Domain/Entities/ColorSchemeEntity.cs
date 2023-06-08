namespace LabPrototype.Domain.Entities
{
    public class ColorSchemeEntity : EntityBase
    {
        public string? Name { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }

        public virtual ICollection<MeasurementTypeEntity>? MeasurementTypes { get; set; }
        public virtual ICollection<MeterTypeEntity>? MeterTypes { get; set; }
    }
}
