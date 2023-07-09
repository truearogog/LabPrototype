using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.MeterTypeSettings
{
    public class CreateMeterTypeDialogViewModel : CreateDialogViewModelBase<
        MeterType, 
        IMeterTypeService, 
        IMeterTypeStore, 
        MeterTypeForm,
        MeterTypeSettingsFormViewModel>
    {
        public CreateMeterTypeDialogViewModel() : base() { }
    }
}
