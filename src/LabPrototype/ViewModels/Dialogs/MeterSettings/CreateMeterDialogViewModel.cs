using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Forms;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;

namespace LabPrototype.ViewModels.Dialogs.MeterSettings
{
    public class CreateMeterDialogViewModel : CreateDialogViewModelBase<
        Meter, 
        IMeterService, 
        IMeterStore, 
        MeterForm,
        MeterSettingsFormViewModel>
    {
        public CreateMeterDialogViewModel() : base() { }
    }
}
