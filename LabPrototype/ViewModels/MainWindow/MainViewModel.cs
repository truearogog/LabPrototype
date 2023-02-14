using LabPrototype.Commands;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.MainWindow
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterStore;
        public bool HasSelectedMeter => _selectedMeterStore.SelectedMeter != null;
        public ViewModelBase CurrentViewModel => HasSelectedMeter ? SelectedMeterViewModel : MeterListingViewModel;

        public MeterListingViewModel MeterListingViewModel { get; }
        public SelectedMeterViewModel SelectedMeterViewModel { get; }

        public ICommand LoadMetersCommand { get; }

        public MainViewModel(IDialogService dialogService, IMeterService meterStore, ISelectedMeterService selectedMeterStore)
        {
            _selectedMeterStore = selectedMeterStore;
            _selectedMeterStore.SubscribeSelectedMeterChanged(SelectedMeterStore_SelectedMeterChanged);

            MeterListingViewModel = new MeterListingViewModel(dialogService, meterStore, selectedMeterStore);
            SelectedMeterViewModel = new SelectedMeterViewModel(selectedMeterStore);

            LoadMetersCommand = new LoadMetersCommand(meterStore);
        }

        private void SelectedMeterStore_SelectedMeterChanged()
        {
            this.RaisePropertyChanged(nameof(HasSelectedMeter));
            this.RaisePropertyChanged(nameof(CurrentViewModel));
        }

        public static MainViewModel LoadViewModel(IDialogService dialogService, IMeterService meterStore, ISelectedMeterService selectedMeterStore)
        {
            var viewModel = new MainViewModel(dialogService, meterStore, selectedMeterStore);
            viewModel.LoadMetersCommand.Execute(null);
            return viewModel;
        }
    }
}
