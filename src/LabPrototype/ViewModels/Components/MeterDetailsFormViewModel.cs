using LabPrototype.Domain.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailsFormViewModel : ViewModelBase
    {
        public Guid Id { get; set; }

        private string _serialCode;
        public string SerialCode
        {
            get => _serialCode;
            set => this.RaiseAndSetIfChanged(ref _serialCode, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MeterDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

        public void Update(Meter meter)
        {
            Id = meter.Id;
            SerialCode = meter.SerialCode;
            Name = meter.Name;
            Address = meter.Address;
        }
    }
}
