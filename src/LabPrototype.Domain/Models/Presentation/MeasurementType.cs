namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementType : PresentationModelBase
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public int ColorSchemeId { get; set; }
    }
}
