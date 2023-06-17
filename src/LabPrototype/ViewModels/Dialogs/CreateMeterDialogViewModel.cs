using LabPrototype.Domain.IServices;
using LabPrototype.ViewModels.Components;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateMeterDialogViewModel : DialogViewModelBase
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public CreateMeterDialogViewModel()
        {
            var meterService = GetRequiredService<IMeterService>();

            MeterDetailFormViewModel = new MeterDetailFormViewModel(CloseCommand, (meterStore, meter) => {
                meterStore.Create(meterService, meter);
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
