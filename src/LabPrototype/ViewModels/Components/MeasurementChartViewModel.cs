using SkiaSharp;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementChartViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IEnabledMeasurementAttributeService _enabledMeasurementAttributeService;
        private readonly IMeasurementService _measurementService;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        //private IDictionary<Guid, ObservableCollection<DateTimePoint>> _attributeValues = new Dictionary<Guid, ObservableCollection<DateTimePoint>>();
        //private IDictionary<Guid, Action<bool>> _attributeEnabledSetters = new Dictionary<Guid, Action<bool>>();

        private bool _hasSelectedLimit = false;
        public bool HasSelectedLimit
        {
            get => _hasSelectedLimit;
            set => this.RaiseAndSetIfChanged(ref _hasSelectedLimit, value);
        }

        private string _minSelectedLimit;
        public string MinSelectedLimit
        {
            get => _minSelectedLimit;
            set => this.RaiseAndSetIfChanged(ref _minSelectedLimit, value);
        }

        private string _maxSelectedLimit;
        public string MaxSelectedLimit 
        {
            get => _maxSelectedLimit;
            set => this.RaiseAndSetIfChanged(ref _maxSelectedLimit, value);
        }

        public MeasurementChartViewModel(
            ISelectedMeterService selectedMeterService,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterUpdated);

            _enabledMeasurementAttributeService = enabledMeasurementAttributeService;
            _enabledMeasurementAttributeService.SubscribeAttributeEnabledChanged(AttributeEnabledChanged);

            _measurementService = measurementService;
            _measurementService.SubscribeMeterMeasurementsLoaded(MeterMeasurementsLoaded);
        }

        public override void Dispose()
        {
            _selectedMeterService.UnsubscribeSelectedMeterUpdated(SelectedMeterUpdated);
            _enabledMeasurementAttributeService.UnsubscribeAttributeEnabledChanged(AttributeEnabledChanged);
            _measurementService.UnsubscribeMeterMeasurementsLoaded(MeterMeasurementsLoaded);
            base.Dispose();
        }

        private async Task CreateSeries()
        {
            //_attributeValues.Clear();
            //_attributeEnabledSetters.Clear();

            if (SelectedMeter != null)
            {

                var measurements = _measurementService.LoadedMeasurements[SelectedMeter.Id];

                foreach (var measurementAttribute in SelectedMeter.MeasurementAttributes)
                {
                    /*
                    _attributeValues[measurementAttribute.Id] = new ObservableCollection<DateTimePoint>(
                        measurements.Select(x => new DateTimePoint(x.DateTime, (double)measurementAttribute.ValueGetter(x)))
                    );

                    var series = new LineSeries<DateTimePoint>
                    {
                        LineSmoothness = 0,
                        Fill = null,
                        Values = _attributeValues[measurementAttribute.Id],
                        Stroke = new SolidColorPaint(SKColor.Parse(measurementAttribute.ColorScheme.Primary), 2),
                        GeometrySize = 0,
                        GeometryStroke = null,
                        GeometryFill = null,
                    };

                    _attributeEnabledSetters[measurementAttribute.Id] = enabled => series.IsVisible = enabled;

                    Series.Add(series);
                    */
                }
            }
        }

        private void SelectedMeterUpdated()
        {
            if (SelectedMeter != null)
            {
                Task.Run(async () => await _measurementService.LoadMeter(SelectedMeter.Id));
            }
        }

        private void MeterMeasurementsLoaded(Guid meterId)
        {
            Task.Run(CreateSeries);
        }

        private void AttributeEnabledChanged(Guid id, bool enabled)
        {
            // _attributeEnabledSetters.TryGetValue(id, out var handler);
            // handler?.Invoke(enabled);
        }

        public void ShowCrosshair()
        {

        }

        public void HideCrosshair()
        {

        }

        public void ShowSelectedLimitLabels()
        {
            HasSelectedLimit = true;
        }

        public void HideSelectedLimitLabels()
        {
            HasSelectedLimit = false;
        }

        public void UpdateSelectedLimitLabels(double minLimitPercentage, double maxLimitPercentage)
        {
            /*
            if (xAxis?.MinLimit.HasValue ?? false)
            {
                var diff = xAxis.MaxLimit - xAxis.MinLimit;
                var minDateTime = new DateTime((long)(xAxis.MinLimit + diff * minLimitPercentage));
                var maxDateTime = new DateTime((long)(xAxis.MaxLimit + diff * maxLimitPercentage));
                var formatting = (DateTime dateTime) => dateTime.ToString("dd/MM/yyyy HH:mm");
                MinSelectedLimit = formatting(minDateTime);
                MaxSelectedLimit = formatting(maxDateTime);
            }
            */
        }
    }
}
