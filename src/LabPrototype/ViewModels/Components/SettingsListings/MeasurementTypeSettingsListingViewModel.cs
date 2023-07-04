using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsListingItems;
using LabPrototype.ViewModels.Dialogs.MeasurementTypeSettings;
using LabPrototype.Views.Dialogs.MeasurementTypeSettings;

namespace LabPrototype.ViewModels.Components.SettingsListings
{
    public class MeasurementTypeSettingsListingViewModel : SettingsListingViewModelBase<
        MeasurementType,
        IMeasurementTypeService,
        IMeasurementTypeStore,
        CreateMeasurementTypeDialog, CreateMeasurementTypeDialogViewModel,
        UpdateMeasurementTypeDialog, UpdateMeasurementTypeDialogViewModel,
        DeleteMeasurementTypeDialog, DeleteMeasurementTypeDialogViewModel,
        MeasurementTypeSettingsListingItemViewModel>
    {
        public MeasurementTypeSettingsListingViewModel(WindowViewModelBase parentWindow) : base(parentWindow)
        {
        }
    }
}
