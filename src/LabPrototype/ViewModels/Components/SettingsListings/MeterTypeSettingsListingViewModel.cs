using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsListingItems;
using LabPrototype.ViewModels.Dialogs.MeterTypeSettings;
using LabPrototype.Views.Dialogs.MeterTypeSettings;

namespace LabPrototype.ViewModels.Components.SettingsListings
{
    public class MeterTypeSettingsListingViewModel : SettingsListingViewModelBase<
        MeterType,
        IMeterTypeService,
        IMeterTypeStore,
        CreateMeterTypeDialog, CreateMeterTypeDialogViewModel,
        UpdateMeterTypeDialog, UpdateMeterTypeDialogViewModel,
        DeleteMeterTypeDialog, DeleteMeterTypeDialogViewModel,
        MeterTypeSettingsListingItemViewModel>
    {
        public MeterTypeSettingsListingViewModel(WindowViewModelBase parentWindow) : base(parentWindow)
        {
        }
    }
}
