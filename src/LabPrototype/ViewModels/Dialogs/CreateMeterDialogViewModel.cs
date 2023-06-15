using LabPrototype.ViewModels.Components;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateMeterDialogViewModel : DialogViewModelBase
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public CreateMeterDialogViewModel()
        {
            MeterDetailFormViewModel = new MeterDetailFormViewModel(CloseCommand, (meterStore, meter) => {
                meterStore.Create(meter);
                Close();
            });
        }

        public override void Dispose()
        {
            MeterDetailFormViewModel.Dispose();

            base.Dispose();
        }
    }
}
