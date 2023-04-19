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
    public class MeterViewModel : ViewModelBase
    {
        private readonly IWindowService _dialogService;
        private readonly IMeterService _meterService;
        public Meter Meter { get; private set; }

        public MeterDetailListingViewModel MeterDetailListingViewModel { get; }
        public FlowMeasurementListingViewModel FlowMeasurementListingViewModel { get; }

        public MeasurementHistoryChartViewModel MeasurementHistoryChartViewModel { get; }
        public MeasurementHistoryTableViewModel MeasurementHistoryTableViewModel { get; }

        public ICommand SelectChartCommand { get; }
        public ICommand SelectTableCommand { get; }
        public ICommand OpenUpdateMeterCommand { get; }
        public ICommand OpenDeleteMeterCommand { get; }

        public MeterViewModel(
            IWindowService dialogService,
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

            MeasurementHistoryChartViewModel = new MeasurementHistoryChartViewModel(selectedMeterService, enabledMeasurementAttributeService, measurementService, chartMeasurementProvider)
            {
                IsVisible = true
            };

            MeasurementHistoryTableViewModel = new MeasurementHistoryTableViewModel(selectedMeterService, measurementService)
            {
                IsVisible = false
            };

            SelectChartCommand = ReactiveCommand.Create(SelectMeasurementChart);
            SelectTableCommand = ReactiveCommand.Create(SelectMeasurementTable);
            OpenUpdateMeterCommand = ReactiveCommand.CreateFromTask(OpenUpdateMeterDialogAsync);
            OpenDeleteMeterCommand = ReactiveCommand.CreateFromTask(OpenDeleteMeterDialogAsync);

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

        private void SelectMeasurementChart()
        {
            MeasurementHistoryChartViewModel.IsVisible = true;
            MeasurementHistoryTableViewModel.IsVisible = false;
        }

        private void SelectMeasurementTable()
        {
            MeasurementHistoryChartViewModel.IsVisible = false;
            MeasurementHistoryTableViewModel.IsVisible = true;
        }

        private async Task OpenUpdateMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(UpdateMeterDialogViewModel), parameter);
        }

        private async Task OpenDeleteMeterDialogAsync()
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
