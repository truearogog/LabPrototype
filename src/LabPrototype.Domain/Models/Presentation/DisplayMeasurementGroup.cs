namespace LabPrototype.Domain.Models.Presentation
{
    public class DisplayMeasurementGroup
    {
        public int MeterId { get; set; }
        public ICollection<DisplayMeasurement>? Measurements { get; set; } = new List<DisplayMeasurement>();
    }
}
