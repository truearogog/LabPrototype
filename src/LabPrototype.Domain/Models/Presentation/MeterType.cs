using LabPrototype.Domain.Entities;

namespace LabPrototype.Domain.Models.Presentation
{
    public class MeterType : PresentationModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public int ColorSchemeId { get; set; }
        public ColorSchemeEntity? ColorScheme { get; set; }
    }
}
