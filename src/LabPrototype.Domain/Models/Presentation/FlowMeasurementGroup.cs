namespace LabPrototype.Domain.Models.Presentation.MeasurementGroups
{
    public class FlowMeasurementGroup
    {
        public int MeterId { get; set; }
        public ICollection<FlowMeasurement>? Measurements { get; set; }
    }
}
