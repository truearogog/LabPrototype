namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementGroup : PresentationModelBase
    {
        public DateTime DateTime { get; set; }
        public IEnumerable<double> AverageValues { get; set; } = Enumerable.Empty<double>();
        public IEnumerable<double> SummaryValues { get; set; } = Enumerable.Empty<double>();
        public int MeasurementGroupArchiveId { get; set; }
    }
}
