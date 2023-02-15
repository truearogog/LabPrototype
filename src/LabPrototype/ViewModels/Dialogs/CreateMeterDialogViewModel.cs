using LabPrototype.Commands;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateMeterDialogViewModel : DialogViewModelBase
    {
        public MeterDetailsFormViewModel MeterDetailsFormViewModel { get; }

        public CreateMeterDialogViewModel(IMeterService meterService)
        {
            ICommand createCommand = new CreateMeterCommand(this, meterService);

            MeterDetailsFormViewModel = new MeterDetailsFormViewModel(createCommand, CloseCommand);
        }
    }
}
