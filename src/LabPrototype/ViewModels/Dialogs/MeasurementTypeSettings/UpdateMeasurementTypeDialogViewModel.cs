using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Forms;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.MeasurementTypeSettings
{
    public class UpdateMeasurementTypeDialogViewModel : UpdateDialogViewModelBase<
        MeasurementType, 
        IMeasurementTypeService, 
        IMeasurementTypeStore, 
        MeasurementTypeForm,
        MeasurementTypeSettingsFormViewModel>
    {
        public UpdateMeasurementTypeDialogViewModel() : base() { }
    }
}
