using LabPrototype.ViewModels.Forms.Base;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;

namespace LabPrototype.ViewModels.Forms
{
    public class MeterFormViewModel : FormViewModelBase
    {
        public bool UseSerialConnection
        {
            get => _useSerialConnection;
            set
            { 
                this.RaiseAndSetIfChanged(ref _useSerialConnection, value);
                this.RaiseAndSetIfChanged(ref _useInternetConnection, !value, nameof(UseInternetConnection));
            }
        }
        private bool _useSerialConnection = true;

        public bool UseInternetConnection
        {
            get => _useInternetConnection;
            set
            {
                this.RaiseAndSetIfChanged(ref _useInternetConnection, value);
                this.RaiseAndSetIfChanged(ref _useSerialConnection, !value, nameof(UseSerialConnection));
            }
        }
        private bool _useInternetConnection = false;

        public MeterInformationForm InformationForm { get; } = new();
        public MeterSerialConnectionForm SerialConnectionForm { get; } = new();
        public MeterInternetConnectionForm InternetConnectionForm { get; } = new();

        public override IEnumerable<ValidationResult> Validate(ValidationContext? validationContext = null)
        {
            var results = new List<ValidationResult>();
            results.AddRange(InformationForm.Validate());
            if (UseSerialConnection)
            {
                results.AddRange(SerialConnectionForm.Validate());
            }
            if (UseInternetConnection)
            {
                results.AddRange(InternetConnectionForm.Validate());
            }
            return results;
        }
    }

    public class MeterInformationForm : FormViewModelBase
    {
        [Required]
        public string SerialCode { get => _serialCode; set => this.RaiseAndSetIfChanged(ref _serialCode, value); }
        private string _serialCode = string.Empty;

        [Required]
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }
        private string _name = string.Empty;

        [Required]
        public string Address { get => _address; set => this.RaiseAndSetIfChanged(ref _address, value); }
        private string _address = string.Empty;

        [Range(1, 247)]
        public int MeterNr { get => _meterNr; set => this.RaiseAndSetIfChanged(ref _meterNr, value); }
        private int _meterNr = 1;
    }

    public class MeterSerialConnectionForm : FormViewModelBase
    {
        [Required, RegularExpression(@"(c|C)(o|O)(m|M)\d+", ErrorMessage = "Input valid serial port name.")]
        public string PortName { get => _portName; set => this.RaiseAndSetIfChanged(ref _portName, value); }
        private string _portName = string.Empty;

        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
    }

    public class MeterInternetConnectionForm : FormViewModelBase
    {
        [Required, RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Input valid IP address.")]
        public string IpAddress { get => _ipAddress; set => this.RaiseAndSetIfChanged(ref _ipAddress, value); }
        private string _ipAddress = string.Empty;

        public int Port { get; set; } = 5000;
    }
}
