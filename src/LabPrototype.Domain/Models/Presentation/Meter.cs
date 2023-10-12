using System.IO.Ports;

namespace LabPrototype.Domain.Models.Presentation
{
    public class Meter : PresentationModelBase, IColorScheme
    {
        public string SerialCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int MeterNr { get; set; }

        public bool UseSerialConnection { get; set; } = true;
        public bool UseInternetConnection { get; set; } = false;

        public string PortName { get; set; } = string.Empty;
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }

        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
    }
}
