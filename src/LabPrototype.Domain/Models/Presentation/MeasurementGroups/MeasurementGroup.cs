using LabPrototype.Domain.Models.Presentation.Measurements;

namespace LabPrototype.Domain.Models.Presentation.MeasurementGroups
{
    public class MeasurementGroup : PresentationModelBase
    {
        public DateTime DateTime { get; set; }
        public int MeasurementGroupArchiveId { get; set; }
        public ICollection<Measurement>? Measurements { get; set; }
    }
}
