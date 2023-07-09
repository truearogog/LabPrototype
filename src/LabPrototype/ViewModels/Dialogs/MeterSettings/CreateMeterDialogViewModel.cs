using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
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
