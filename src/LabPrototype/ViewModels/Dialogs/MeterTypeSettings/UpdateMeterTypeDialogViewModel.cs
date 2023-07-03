using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.MeterTypeSettings
{
    public class UpdateMeterTypeDialogViewModel : UpdateDialogViewModelBase<MeterType, IMeterTypeService, IMeterTypeStore, MeterTypeSettingsFormViewModel>
    {
        public UpdateMeterTypeDialogViewModel() : base() { }
    }
}
