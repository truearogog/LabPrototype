namespace LabPrototype.Domain.Models.Presentation
{
    public class MeterType : PresentationModelBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ColorSchemeId { get; set; }
    }
}
