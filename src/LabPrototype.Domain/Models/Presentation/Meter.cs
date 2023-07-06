using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Domain.Models.Presentation
{
    public class Meter : PresentationModelBase
    {
        public string SerialCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int MeterTypeId { get; set; }
        public MeterType? MeterType { get; set; }
    }
}
