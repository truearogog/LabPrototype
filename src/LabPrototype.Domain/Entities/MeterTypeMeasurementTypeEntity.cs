namespace LabPrototype.Domain.Entities
{
    public class MeterTypeMeasurementTypeEntity : EntityBase
    {
        public int MeterTypeId { get; set; }
        public MeterTypeEntity? MeterType { get; set; }

        public int MeasurementTypeId { get; set; }
        public MeasurementTypeEntity? MeasurementType { get; set; }
    }
}
