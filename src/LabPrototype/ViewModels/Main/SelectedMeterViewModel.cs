using LabPrototype.Domain.Models;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Dialogs;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class SelectedMeterViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IMeterService _meterService;
        private readonly ISelectedMeterService _selectedMeterService;

        public Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public MeterDetailListingViewModel MeterDetailListingViewModel { get; }
        public HistoricMeasurementChartViewModel HistoricMeasurementChartViewModel { get; }
        public FlowMeasurementListingViewModel FlowMeasurementListingViewModel { get; }

        public ICommand OpenUpdateMeterCommand { get; }
        public ICommand OpenDeleteMeterCommand { get; }

        public SelectedMeterViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedMeterService, 
            IFlowMeasurementProvider flowMeasurementProvider,
            IChartMeasurementProvider chartMeasurementProvider,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            _dialogService = dialogService;
            _meterService = meterService;
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            MeterDetailListingViewModel = new MeterDetailListingViewModel(selectedMeterService);
            FlowMeasurementListingViewModel = new FlowMeasurementListingViewModel(selectedMeterService, flowMeasurementProvider);
            HistoricMeasurementChartViewModel = new HistoricMeasurementChartViewModel(
                selectedMeterService,
                chartMeasurementProvider, 
                enabledMeasurementAttributeService,
                measurementService);

            OpenUpdateMeterCommand = ReactiveCommand.CreateFromTask(ShowUpdateMeterDialogAsync);
            OpenDeleteMeterCommand = ReactiveCommand.CreateFromTask(ShowDeleteMeterDialogAsync);

            if (!flowMeasurementProvider.IsRunning)
            {
                flowMeasurementProvider.Start();
            }
        }

        public override void Dispose()
        {
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            base.Dispose();
        }

        private async Task ShowUpdateMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(UpdateMeterDialogViewModel), parameter);
        }

        private async Task ShowDeleteMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(DeleteMeterDialogViewModel), parameter);
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            this.RaisePropertyChanged(nameof(SelectedMeter));
        }
    }
}
