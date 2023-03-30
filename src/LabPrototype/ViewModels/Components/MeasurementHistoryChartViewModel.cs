using System;
using System.Linq;
using System.Threading.Tasks;
using LabPrototype.Extensions;
using LabPrototype.Domain.Models;
using LabPrototype.Models.Interfaces;
using LabPrototype.Services.Interfaces;
using System.Windows.Input;
using ReactiveUI;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryChartViewModel : MeasurementHistoryViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IEnabledMeasurementAttributeService _enabledMeasurementAttributeService;
        private readonly IMeasurementService _measurementService;
        private readonly IChartMeasurementProvider _chartMeasurementProvider;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }

        public IPlotProvider PlotProvider { get; set; }

        private bool _lockXAxis;
        public bool LockXAxis
        {
            get => _lockXAxis;
            set
            {
                PlotProvider.LockXAxis = value;
                this.RaiseAndSetIfChanged(ref _lockXAxis, value);
            }
        }

        private bool _lockYAxis;
        public bool LockYAxis
        {
            get => _lockYAxis;
            set
            {
                PlotProvider.LockYAxis = value;
                this.RaiseAndSetIfChanged(ref _lockYAxis, value);
            }
        }

        public ICommand ToggleLockXAxisCommand { get; }
        public ICommand ToggleLockYAxisCommand { get; }

        public MeasurementHistoryChartViewModel(
            ISelectedMeterService selectedMeterService,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService,
            IChartMeasurementProvider chartMeasurementProvider)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            _enabledMeasurementAttributeService = enabledMeasurementAttributeService;
            _enabledMeasurementAttributeService.AttributeEnabledChanged += _AttributeEnabledChanged;

            _measurementService = measurementService;

            _chartMeasurementProvider = chartMeasurementProvider;

            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel(
                selectedMeterService,
                chartMeasurementProvider,
                enabledMeasurementAttributeService);

            ToggleLockXAxisCommand = ReactiveCommand.Create(() => LockXAxis = !LockXAxis);
            ToggleLockYAxisCommand = ReactiveCommand.Create(() => LockYAxis = !LockYAxis);
        }

        public override void Dispose()
        {
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            _enabledMeasurementAttributeService.AttributeEnabledChanged -= _AttributeEnabledChanged;
            base.Dispose();
        }

        public void UpdateNearestMeasurement(int nearestIndex)
        {
            if (SelectedMeter != null)
            {
                var measurement = _measurementService.LoadedMeasurements[SelectedMeter.Id].ElementAt(nearestIndex);
                _chartMeasurementProvider.Measurement = measurement;
            }
        }

        private void CreateSeries()
        {
            PlotProvider.ClearPlots();
            if (SelectedMeter != null)
            {
                var measurements = _measurementService.LoadedMeasurements[SelectedMeter.Id];

                var plotIds = SelectedMeter.MeasurementAttributes.Select(a => a.Id).ToArray();
                var xs = measurements.Select(x => x.DateTime.ToOADate()).ToArray();
                var ys = SelectedMeter.MeasurementAttributes.Select(a => measurements.Select(m => (double)a.ValueGetter(m)).ToArray()).ToArray();
                var colors = SelectedMeter.MeasurementAttributes.Select(a => a.ColorScheme.Primary.ToColor()).ToArray();

                PlotProvider.AddPlots(plotIds, xs, ys, colors);
            }
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            if (SelectedMeter != null)
            {
                Task.Run(async () => {
                    await _measurementService.LoadMeter(meter.Id);
                    CreateSeries();
                    PlotProvider.AddCrosshair();
                }).Wait();
            }
        }

        private void _AttributeEnabledChanged(Guid attributeId, bool enabled)
        {
            PlotProvider?.SetPlotVisibility(attributeId, enabled);
        }
    }
}
