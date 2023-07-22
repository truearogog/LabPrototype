namespace LabPrototype.Domain.Models.Presentation.Measurements
{
    public class Measurement : PresentationModelBase
    {
        public double Average { get; set; }
        public double Summary { get; set; }
        public int MeasurementGroupId { get; set; }
        public int MeasurementTypeId { get; set; }
    }
}
