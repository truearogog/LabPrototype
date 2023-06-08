namespace LabPrototype.Domain.Models.Presentation
{
    public class Measurement : PresentationModelBase
    {
        public double Value { get; set; }
        public int MeasurementTypeId { get; set; }
        public int MeasurementGroupId { get; set; }
    }
}
