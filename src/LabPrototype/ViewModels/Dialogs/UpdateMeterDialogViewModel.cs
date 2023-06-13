using LabPrototype.Domain.IStores;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Models;

namespace LabPrototype.ViewModels.Dialogs
{
    public class UpdateMeterDialogViewModel : ParametrizedDialogViewModelBase<MeterNavigationParameter>
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public UpdateMeterDialogViewModel()
        {
            IMeterStore meterService = GetRequiredService<IMeterStore>();

            MeterDetailFormViewModel = new MeterDetailFormViewModel(CloseCommand, meterService.Update);
        }

        public override void Dispose()
        {
            MeterDetailFormViewModel.Dispose();

            base.Dispose();
        }

        public override void Activate(MeterNavigationParameter parameter)
        {
            MeterDetailFormViewModel.Meter = parameter.Meter;
        }
    }
}
