using LabPrototype.Commands;
using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class DeleteMeterDialogViewModel : ParameterizedDialogViewModelBase<MeterNavigationParameter>
    {
        private Meter _meter;
        public Meter Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeleteMeterDialogViewModel(IMeterService meterService)
        {
            DeleteCommand = new DeleteMeterCommand(this, meterService);
            CancelCommand = CloseCommand;
        }

        public override void Activate(MeterNavigationParameter parameter)
        {
            Meter = parameter.Meter;
        }
    }
}
