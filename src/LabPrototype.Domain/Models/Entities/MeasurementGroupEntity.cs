namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementGroupEntity : EntityBase
    {        
        public DateTime DateTime { get; set; }
        public IEnumerable<double> AverageValues { get; set; } = Enumerable.Empty<double>();
        public IEnumerable<double> SummaryValues { get; set; } = Enumerable.Empty<double>();

        public int MeasurementGroupArchiveId { get; set; }
        public virtual ArchiveEntity? MeasurementGroupArchive { get; set; }
    }
}
