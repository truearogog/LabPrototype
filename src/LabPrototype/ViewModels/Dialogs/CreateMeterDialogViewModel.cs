using LabPrototype.Domain.IStores;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateMeterDialogViewModel : DialogViewModelBase<DialogResultBase>
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public CreateMeterDialogViewModel()
        {
            IMeterStore meterService = GetRequiredService<IMeterStore>();

            MeterDetailFormViewModel = new MeterDetailFormViewModel(CloseCommand, meterService.Create);
        }

        public override void Dispose()
        {
            MeterDetailFormViewModel.Dispose();

            base.Dispose();
        }
    }
}
