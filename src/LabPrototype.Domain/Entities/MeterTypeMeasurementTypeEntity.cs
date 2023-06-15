namespace LabPrototype.Domain.Entities
{
    public class MeterTypeMeasurementTypeEntity : EntityBase
    {
        public int MeterTypeId { get; set; }
        public virtual MeterTypeEntity? MeterType { get; set; }

        public int MeasurementTypeId { get; set; }
        public virtual MeasurementTypeEntity? MeasurementType { get; set; }
    }
}
