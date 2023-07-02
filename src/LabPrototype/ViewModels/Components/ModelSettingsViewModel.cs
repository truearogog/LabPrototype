using LabPrototype.ViewModels.Components.Settings;
using LabPrototype.ViewModels.Components.SettingsListings;

namespace LabPrototype.ViewModels.Components
{
    public class ModelSettingsViewModel : ViewModelBase
    {
        public MeterSettingsListingViewModel MeterSettingsListingViewModel { get; set; }
        public ColorSchemeSettingsListingViewModel ColorSchemeSettingsListingViewModel { get; set; }
        public MeasurementTypeSettingsListingViewModel MeasurementTypeSettingsListingViewModel { get; set; }

        public ModelSettingsViewModel(WindowViewModelBase parentWindow)
        {
            MeterSettingsListingViewModel = new MeterSettingsListingViewModel(parentWindow);
            ColorSchemeSettingsListingViewModel = new ColorSchemeSettingsListingViewModel(parentWindow);
            MeasurementTypeSettingsListingViewModel = new MeasurementTypeSettingsListingViewModel(parentWindow);
        }
    }
}
