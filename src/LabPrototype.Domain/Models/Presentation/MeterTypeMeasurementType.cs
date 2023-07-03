namespace LabPrototype.Domain.Models.Presentation
{
    public class MeterTypeMeasurementType : PresentationModelBase
    {
        public int MeterTypeId { get; set; }
        public int MeasurementTypeId { get; set; }
        public MeasurementType? MeasurementType { get; set; }
        public int SortOrder { get; set; }
    }
}
