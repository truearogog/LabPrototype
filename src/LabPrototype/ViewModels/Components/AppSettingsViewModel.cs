namespace LabPrototype.ViewModels.Components
{
    public class AppSettingsViewModel : ViewModelBase
    {
        public Settings.MeterListingViewModel MeterListingViewModel { get; set; }

        public AppSettingsViewModel(WindowViewModelBase parentWindow)
        {
            MeterListingViewModel = new(parentWindow);
        }
    }
}
