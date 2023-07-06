namespace LabPrototype.Domain.Models.Presentation
{
    public class ColorScheme : PresentationModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
    }
}
