using LabPrototype.Commands;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        public bool HasSelectedMeter => _selectedMeterService.SelectedMeter != null;
        public ViewModelBase CurrentViewModel => HasSelectedMeter ? SelectedMeterViewModel : MeterListingViewModel;

        public MeterListingViewModel MeterListingViewModel { get; }
        public SelectedMeterViewModel SelectedMeterViewModel { get; }

        public ICommand LoadMetersCommand { get; }
        public ICommand DeselectMeterCommand { get; }

        public MainViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedmeterService, 
            IFlowMeasurementProvider flowMeasurementProvider)
        {
            _selectedMeterService = selectedmeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            MeterListingViewModel = new MeterListingViewModel(dialogService, meterService, selectedmeterService);
            SelectedMeterViewModel = new SelectedMeterViewModel(dialogService, meterService, selectedmeterService, flowMeasurementProvider);

            LoadMetersCommand = new LoadMetersCommand(meterService);
            DeselectMeterCommand = new DeselectMeterCommand(selectedmeterService);
        }

        private void SelectedMeterService_SelectedMeterUpdated()
        {
            this.RaisePropertyChanged(nameof(HasSelectedMeter));
            this.RaisePropertyChanged(nameof(CurrentViewModel));
        }

        public static MainViewModel LoadViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedmeterService, 
            IFlowMeasurementProvider flowMeasurementProvider)
        {
            var viewModel = new MainViewModel(dialogService, meterService, selectedmeterService, flowMeasurementProvider);
            viewModel.LoadMetersCommand.Execute(null);
            return viewModel;
        }
    }
}
