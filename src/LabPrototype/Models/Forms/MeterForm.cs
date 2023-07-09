using System.ComponentModel.DataAnnotations;
using System.IO.Ports;

namespace LabPrototype.Models.Forms
{
    public class MeterForm : FormBase
    {
        [Required]
        public string SerialCode { get => _serialCode; set => ValidateAndSetThrow(ref _serialCode, value); }
        private string _serialCode = string.Empty;

        [Required]
        public string Name { get => _name; set => ValidateAndSetThrow(ref _name, value); }
        private string _name = string.Empty;

        [Required]
        public string Address { get => _address; set => ValidateAndSetThrow(ref _address, value); }
        private string _address = string.Empty;

        [Range(1, 256)]
        public int MeterNr { get; set; } = 1;

        [Required, RegularExpression(@"(c|C)(o|O)(m|M)\d+")]
        public string PortName { get => _portName; set => ValidateAndSetThrow(ref _portName, value); }
        private string _portName = "COM4";

        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;

        public int MeterTypeId { get; set; }
    }
}
