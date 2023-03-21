using System;
using System.Linq;
using System.Threading.Tasks;
using LabPrototype.Extensions;
using LabPrototype.Domain.Models;
using LabPrototype.Models.Interfaces;
using LabPrototype.Services.Interfaces;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementChartViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IEnabledMeasurementAttributeService _enabledMeasurementAttributeService;
        private readonly IMeasurementService _measurementService;
        private readonly IChartMeasurementProvider _chartMeasurementProvider;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public IPlotProvider PlotProvider { get; set; }

        public MeasurementChartViewModel(
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
                var xs = measurements.Select(x => x.DateTime.ToOADate()).ToArray();
                foreach (var measurementAttribute in SelectedMeter.MeasurementAttributes)
                {
                    var ys = measurements.Select(x => (double)measurementAttribute.ValueGetter(x)).ToArray();
                    var color = measurementAttribute.ColorScheme.Primary.ToColor();
                    PlotProvider.AddPlot(measurementAttribute.Id, xs, ys, color);
                }
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
            PlotProvider.SetPlotVisibility(attributeId, enabled);
        }
    }
}
