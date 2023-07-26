namespace LabPrototype.Domain.Models.Entities
{
    public class ColorSchemeEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;

        public virtual ICollection<MeasurementTypeEntity> MeasurementTypes { get; set; } 
            = new List<MeasurementTypeEntity>();
        public virtual ICollection<MeterTypeEntity> MeterTypes { get; set; } 
            = new List<MeterTypeEntity>();
    }
}
