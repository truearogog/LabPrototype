using LabPrototype.Commands;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateMeterDialogViewModel : DialogViewModelBase<DialogResultBase>
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public CreateMeterDialogViewModel()
        {
            ICommand createCommand = new CreateMeterCommand(this, GetRequiredService<IMeterService>());

            MeterDetailFormViewModel = new MeterDetailFormViewModel(createCommand, CloseCommand);
        }
    }
}
