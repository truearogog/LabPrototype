using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.ModelSettings;

namespace LabPrototype.ViewModels.Dialogs.MeterSettings
{
    public class UpdateMeterDialogViewModel : UpdateDialogViewModelBase<
        Meter, 
        IMeterService, 
        IMeterStore,
        MeterForm,
        MeterSettingsFormViewModel>
    {
        public UpdateMeterDialogViewModel() : base() { }
    }
}
