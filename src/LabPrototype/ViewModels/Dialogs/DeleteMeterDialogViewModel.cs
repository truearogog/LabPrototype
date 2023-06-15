using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class DeleteMeterDialogViewModel : ParametrizedDialogViewModelBase<MeterNavigationParameter>
    {
        private Meter _meter = new();
        public Meter Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        private readonly sbyte _value;

        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public DeleteMeterDialogViewModel()
        {
            var meterStore = GetRequiredService<IMeterStore>();

            CancelCommand = CloseCommand;
            DeleteCommand = ReactiveCommand.Create(() => { 
                meterStore.Delete(_meter.Id);
                Close();
            });
        }

        public override void Activate(MeterNavigationParameter parameter)
        {
            Meter = parameter.Meter ?? throw new Exception();
        }
    }
}
