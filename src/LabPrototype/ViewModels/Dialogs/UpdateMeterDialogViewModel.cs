using LabPrototype.Commands;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Models;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class UpdateMeterDialogViewModel : ParametrizedDialogViewModelBase<MeterNavigationParameter>
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public UpdateMeterDialogViewModel()
        {
            IMeterService meterService = GetRequiredService<IMeterService>();

            ICommand updateCommand = new UpdateMeterCommand(this, meterService);

            MeterDetailFormViewModel = new MeterDetailFormViewModel(updateCommand, CloseCommand);
        }

        public override void Activate(MeterNavigationParameter parameter)
        {
            MeterDetailFormViewModel.Update(parameter.Meter);
        }
    }
}
