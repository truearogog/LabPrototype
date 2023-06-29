using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsListingItems;
using LabPrototype.ViewModels.Dialogs.MeterSettings;
using LabPrototype.Views.Dialogs.MeterSettings;

namespace LabPrototype.ViewModels.Components.Settings
{
    public class MeterSettingsListingViewModel : SettingsListingViewModelBase<
        Meter,
        IMeterService,
        IMeterStore,
        CreateMeterDialog, CreateMeterDialogViewModel,
        UpdateMeterDialog, UpdateMeterDialogViewModel,
        DeleteMeterDialog, DeleteMeterDialogViewModel,
        MeterSettingsListingItemViewModel>
    {
        public MeterSettingsListingViewModel(WindowViewModelBase parentWindow) : base(parentWindow)
        {

        }
    }
}
