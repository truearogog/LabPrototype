namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementType : PresentationModelBase, IColorScheme
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
    }
}
