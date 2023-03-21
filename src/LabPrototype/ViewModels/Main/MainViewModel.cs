using LabPrototype.Commands;
using LabPrototype.Domain.Models;
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
            IChartMeasurementProvider chartMeasurementProvider,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            _selectedMeterService = selectedmeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            MeterListingViewModel = new MeterListingViewModel(dialogService, meterService, selectedmeterService);
            SelectedMeterViewModel = new SelectedMeterViewModel(
                dialogService, 
                meterService, 
                selectedmeterService, 
                flowMeasurementProvider,
                chartMeasurementProvider,
                enabledMeasurementAttributeService,
                measurementService);

            LoadMetersCommand = new LoadMetersCommand(meterService);
            DeselectMeterCommand = new DeselectMeterCommand(selectedmeterService);
        }

        public override void Dispose()
        {
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            base.Dispose();
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            this.RaisePropertyChanged(nameof(HasSelectedMeter));
            this.RaisePropertyChanged(nameof(CurrentViewModel));
        }

        public static MainViewModel LoadViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedmeterService, 
            IFlowMeasurementProvider flowMeasurementProvider,
            IChartMeasurementProvider chartMeasurementProvider,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            var viewModel = new MainViewModel(
                dialogService, 
                meterService, 
                selectedmeterService, 
                flowMeasurementProvider,
                chartMeasurementProvider,
                enabledMeasurementAttributeService,
                measurementService);
            viewModel.LoadMetersCommand.Execute(null);
            return viewModel;
        }
    }
}
