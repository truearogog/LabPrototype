using LabPrototype.Commands;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateMeterDialogViewModel : DialogViewModelBase
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public CreateMeterDialogViewModel(IMeterService meterService)
        {
            ICommand createCommand = new CreateMeterCommand(this, meterService);

            MeterDetailFormViewModel = new MeterDetailFormViewModel(createCommand, CloseCommand);
        }
    }
}
