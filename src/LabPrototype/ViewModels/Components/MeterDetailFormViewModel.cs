using LabPrototype.Domain.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailFormViewModel : ViewModelBase
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

        private int _selectedMeterTypeIndex;
        public int SelectedMeterTypeIndex
        {
            get => _selectedMeterTypeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedMeterTypeIndex, value);
        }

        public ObservableCollection<MeterTypeViewModel> MeterTypes { get; set; } = new();

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MeterDetailFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;

            CreateMeterTypes();
        }

        private void CreateMeterTypes()
        {
            MeterTypes.Clear();
            foreach (var meterType in MeterType.All)
            {
                MeterTypes.Add(new MeterTypeViewModel(meterType));
            }
        }

        public void Update(Meter meter)
        {
            Id = meter.Id;
            SerialCode = meter.SerialCode;
            Name = meter.Name;
            Address = meter.Address;
            SelectedMeterTypeIndex = MeterType.All.FindIndex(x => x.Id.Equals(meter.TypeId));
        }
    }
}
