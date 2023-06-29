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
        public ModelSettingsViewModel ModelSettingsViewModel { get; }

        public ICommand SelectMeterListingCommand { get; }
        public ICommand SelectModelSettingsCommand { get; }

        public MainWindowViewModel()
        {
            MeterListingViewModel = new MeterListingViewModel(this);
            ModelSettingsViewModel = new ModelSettingsViewModel(this);
            CurrentViewModel = MeterListingViewModel;

            SelectMeterListingCommand = ReactiveCommand.Create(() => CurrentViewModel = MeterListingViewModel);
            SelectModelSettingsCommand = ReactiveCommand.Create(() => CurrentViewModel = ModelSettingsViewModel);
        }

        public override void Dispose()
        {
            MeterListingViewModel.Dispose();

            base.Dispose();
        }
    }
}