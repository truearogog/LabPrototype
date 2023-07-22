namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementGroupEntity : EntityBase
    {        
        public DateTime DateTime { get; set; }

        public int MeasurementGroupArchiveId { get; set; }
        public virtual MeasurementGroupArchiveEntity? MeasurementGroupArchive { get; set; }

        public virtual ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    }
}
