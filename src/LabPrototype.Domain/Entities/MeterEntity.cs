namespace LabPrototype.Domain.Entities
{
    public class MeterEntity : EntityBase
    {
        public string SerialCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public int MeterTypeId { get; set; }
        public virtual MeterTypeEntity? MeterType { get; set; }

        public virtual ICollection<MeasurementGroupEntity> MeasurementGroups { get; set; } = new List<MeasurementGroupEntity>();
    }
}
