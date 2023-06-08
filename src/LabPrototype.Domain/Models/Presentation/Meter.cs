namespace LabPrototype.Domain.Models.Presentation
{
    public class Meter : PresentationModelBase
    {
        public string? SerialCode { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int MeterTypeId { get; set; }
    }
}
