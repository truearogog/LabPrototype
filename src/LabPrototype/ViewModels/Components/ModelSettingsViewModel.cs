using LabPrototype.ViewModels.Components.SettingsListings;

namespace LabPrototype.ViewModels.Components
{
    public class ModelSettingsViewModel : ViewModelBase
    {
        public MeterSettingsListingViewModel MeterSettingsListingViewModel { get; set; }
        public MeterTypeSettingsListingViewModel MeterTypeSettingsListingViewModel { get; set; }
        public MeasurementTypeSettingsListingViewModel MeasurementTypeSettingsListingViewModel { get; set; }
        public ColorSchemeSettingsListingViewModel ColorSchemeSettingsListingViewModel { get; set; }

        public ModelSettingsViewModel(WindowViewModelBase parentWindow)
        {
            MeterSettingsListingViewModel = new MeterSettingsListingViewModel(parentWindow);
            MeterTypeSettingsListingViewModel = new MeterTypeSettingsListingViewModel(parentWindow);
            MeasurementTypeSettingsListingViewModel = new MeasurementTypeSettingsListingViewModel(parentWindow);
            ColorSchemeSettingsListingViewModel = new ColorSchemeSettingsListingViewModel(parentWindow);
        }
    }
}
