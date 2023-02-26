using LiveChartsCore.Defaults;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using LabPrototype.Services.Interfaces;
using LabPrototype.Domain.Models;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementChartViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IEnabledMeasurementAttributeService _enabledMeasurementAttributeService;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        private int _currentScaleIndex = 0;
        private readonly List<TimeSpan> _scales = new List<TimeSpan>()
        {
            TimeSpan.FromDays(30),
            TimeSpan.FromDays(7),
            TimeSpan.FromDays(1),
            TimeSpan.FromHours(12),
            TimeSpan.FromHours(6),
        };

        private IDictionary<Guid, ObservableCollection<DateTimePoint>> _attributeValues = new Dictionary<Guid, ObservableCollection<DateTimePoint>>();
        private IDictionary<Guid, Action<bool>> _attributeEnabledSetters = new Dictionary<Guid, Action<bool>>();
        public ObservableCollection<ISeries> Series { get; } = new ObservableCollection<ISeries>();

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                TextSize = 12,
                Labeler = (value) => new DateTime((long)value).ToString("dd/MM/yyyy"),
                MinStep = TimeSpan.FromDays(1).Ticks,
                ForceStepToMin = false,
            }
        };

        public Axis[] YAxes { get; set; } =
        {
            new Axis
            {
                
            }
        };

        private Random _random = new Random();

        public MeasurementChartViewModel(
            ISelectedMeterService selectedMeterService,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            _enabledMeasurementAttributeService = enabledMeasurementAttributeService;
            _enabledMeasurementAttributeService.SubscribeAttributeEnabledChanged(EnabledMeasurementAttributeService_AttributeEnabledChanged);

            CreateSeries();
        }

        private void CreateSeries()
        {
            _attributeValues.Clear();
            _attributeEnabledSetters.Clear();
            Series.Clear();
            if (SelectedMeter != null)
            {
                foreach (var measurementAttribute in SelectedMeter.MeasurementAttributes)
                {
                    var values = new ObservableCollection<DateTimePoint>();
                    var datetime = DateTime.Now.Date;
                    var y = 0;
                    for (int i = 0; i < 1000; ++i)
                    {
                        values.Add(new DateTimePoint(datetime, y));
                        datetime = datetime.AddMinutes(30);
                        y += _random.Next(-2, 3);
                    }
                    _attributeValues[measurementAttribute.Id] = values;

                    var series = new LineSeries<DateTimePoint>
                    {
                        LineSmoothness = .3d,
                        Fill = null,
                        Values = _attributeValues[measurementAttribute.Id],
                        Stroke = new SolidColorPaint(SKColor.Parse(measurementAttribute.ColorScheme.Primary), 2),
                        GeometryStroke = new SolidColorPaint(SKColor.Parse(measurementAttribute.ColorScheme.Primary), 1),
                        GeometrySize = 0,
                        TooltipLabelFormatter = (chartPoint) => $"{new DateTime((long)chartPoint.SecondaryValue):dd/MM HH:mm}: {chartPoint.PrimaryValue}",
                    };

                    _attributeEnabledSetters[measurementAttribute.Id] = (enabled) => series.IsVisible = enabled;

                    Series.Add(series);
                }

                UpdateLimitsToMax();
            }
        }

        public void PlusLimit()
        {
            _currentScaleIndex = Math.Min(_scales.Count - 1, _currentScaleIndex + 1);
            UpdateVisibleLimits();
        }

        public void MinusLimit()
        {
            _currentScaleIndex = Math.Max(0, _currentScaleIndex - 1);
            UpdateVisibleLimits();
        }

        private void UpdateVisibleLimits()
        {
            var xAxis = XAxes.First();
            var avg = (xAxis.VisibleDataBounds.Min + xAxis.VisibleDataBounds.Max) / 2;
            xAxis.MaxLimit = avg + _scales[_currentScaleIndex].Ticks / 2;
            xAxis.MinLimit = avg - _scales[_currentScaleIndex].Ticks / 2;
        }

        private void UpdateLimitsToMax()
        {
            _currentScaleIndex = 0;
            var xAxis = XAxes.First();
            xAxis.MaxLimit = _attributeValues.Max(x => x.Value.Last().DateTime.Ticks);
            xAxis.MinLimit = xAxis.MaxLimit - _scales[_currentScaleIndex].Ticks;
        }

        private void SelectedMeterService_SelectedMeterUpdated()
        {
            CreateSeries();
        }

        private void EnabledMeasurementAttributeService_AttributeEnabledChanged(Guid id, bool enabled)
        {
            _attributeEnabledSetters.TryGetValue(id, out var handler);
            handler?.Invoke(enabled);
        }
    }
}
