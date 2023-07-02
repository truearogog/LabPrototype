using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.MeasurementTypeSettings
{
    public class CreateMeasurementTypeDialogViewModel : CreateDialogViewModelBase<MeasurementType, IMeasurementTypeService, IMeasurementTypeStore, MeasurementTypeSettingsFormViewModel>
    {
        public CreateMeasurementTypeDialogViewModel() : base() { }
    }
}
