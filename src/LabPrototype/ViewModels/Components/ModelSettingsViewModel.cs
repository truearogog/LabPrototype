using LabPrototype.ViewModels.Components.Settings;

namespace LabPrototype.ViewModels.Components
{
    public class ModelSettingsViewModel : ViewModelBase
    {
        public MeterSettingsListingViewModel MeterSettingsListingViewModel { get; set; }

        public ModelSettingsViewModel(WindowViewModelBase parentWindow)
        {
            MeterSettingsListingViewModel = new MeterSettingsListingViewModel(parentWindow);
        }
    }
}
