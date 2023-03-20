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
            IFlowMeasurementProvider flowMeasurementProvider,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            _selectedMeterService = selectedmeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterUpdated);

            MeterListingViewModel = new MeterListingViewModel(dialogService, meterService, selectedmeterService);
            SelectedMeterViewModel = new SelectedMeterViewModel(
                dialogService, 
                meterService, 
                selectedmeterService, 
                flowMeasurementProvider, 
                enabledMeasurementAttributeService,
                measurementService);

            LoadMetersCommand = new LoadMetersCommand(meterService);
            DeselectMeterCommand = new DeselectMeterCommand(selectedmeterService);
        }

        private void SelectedMeterUpdated()
        {
            this.RaisePropertyChanged(nameof(HasSelectedMeter));
            this.RaisePropertyChanged(nameof(CurrentViewModel));
        }

        public static MainViewModel LoadViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedmeterService, 
            IFlowMeasurementProvider flowMeasurementProvider,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            var viewModel = new MainViewModel(
                dialogService, 
                meterService, 
                selectedmeterService, 
                flowMeasurementProvider, 
                enabledMeasurementAttributeService,
                measurementService);
            viewModel.LoadMetersCommand.Execute(null);
            return viewModel;
        }
    }
}
