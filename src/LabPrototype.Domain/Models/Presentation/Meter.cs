using System.IO.Ports;

namespace LabPrototype.Domain.Models.Presentation
{
    public class Meter : PresentationModelBase
    {
        public string SerialCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int MeterNr { get; set; }
        public string PortName { get; set; } = string.Empty;
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public int MeterTypeId { get; set; }
        public MeterType? MeterType { get; set; }
    }
}
