namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementType : PresentationModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int ColorSchemeId { get; set; }
        public ColorScheme? ColorScheme { get; set; }
    }
}
