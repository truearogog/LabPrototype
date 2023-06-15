namespace LabPrototype.Domain.Entities
{
    public class MeasurementGroupEntity : EntityBase
    {
        public int MeterId { get; set; }
        public virtual MeterEntity? Meter { get; set; }

        public virtual ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    }
}
