using LabPrototype.Commands;
using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class UpdateMeterDialogViewModel : ParameterizedDialogViewModelBase<MeterNavigationParameter>
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public UpdateMeterDialogViewModel(IMeterService meterService)
        {
            ICommand updateCommand = new UpdateMeterCommand(this, meterService);

            MeterDetailFormViewModel = new MeterDetailFormViewModel(updateCommand, CloseCommand);
        }

        public override void Activate(MeterNavigationParameter parameter)
        {
            MeterDetailFormViewModel.Update(parameter.Meter);
        }
    }
}
