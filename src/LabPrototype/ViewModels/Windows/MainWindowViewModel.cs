using LabPrototype.ViewModels.Components;

namespace LabPrototype.ViewModels.Main
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MeterListingViewModel MeterListingViewModel { get; }

        public MainWindowViewModel()
        {
            MeterListingViewModel = new MeterListingViewModel(this);
        }

        public override void Dispose()
        {
            MeterListingViewModel.Dispose();

            base.Dispose();
        }
    }
}