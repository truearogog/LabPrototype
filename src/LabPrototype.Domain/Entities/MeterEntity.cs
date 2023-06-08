namespace LabPrototype.Domain.Entities
{
    public class MeterEntity : EntityBase
    {
        public string? SerialCode { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public int MeterTypeId { get; set; }
        public virtual MeterTypeEntity? MeterType { get; set; }

        public virtual ICollection<MeasurementGroupEntity>? MeasurementGroups { get; set; }
    }
}
