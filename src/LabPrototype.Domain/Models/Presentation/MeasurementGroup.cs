namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementGroup : PresentationModelBase
    {
        public int MeterId { get; set; }
        public ICollection<Measurement>? Measurements { get; set; }
    }
}
