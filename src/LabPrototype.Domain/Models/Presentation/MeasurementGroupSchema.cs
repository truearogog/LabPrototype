namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementGroupSchema : PresentationModelBase
    {
        public int MeterTypeId { get; set; }
        public IEnumerable<MeasurementGroupSchemaMeasurementType> MeasurementGroupSchemaMeasurementTypes { get; set; }
            = Enumerable.Empty<MeasurementGroupSchemaMeasurementType>();
    }
}
