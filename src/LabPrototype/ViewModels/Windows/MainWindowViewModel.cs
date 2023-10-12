using LabPrototype.ViewModels.Components;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        public MeterListingViewModel MeterListingViewModel { get; }
        public AppSettingsViewModel AppSettingsViewModel { get; }

        public ICommand SelectMeterListingCommand { get; }
        public ICommand SelectSettingsCommand { get; }

        public MainWindowViewModel()
        {
            MeterListingViewModel = new MeterListingViewModel(this);
            AppSettingsViewModel = new AppSettingsViewModel(this);
            CurrentViewModel = MeterListingViewModel;

            SelectMeterListingCommand = ReactiveCommand.Create(() => CurrentViewModel = MeterListingViewModel);
            SelectSettingsCommand = ReactiveCommand.Create(() => CurrentViewModel = AppSettingsViewModel);
        }

        public override void Dispose()
        {
            MeterListingViewModel.Dispose();

            base.Dispose();
        }
    }
}