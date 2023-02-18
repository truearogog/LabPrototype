using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Main
{
    public class HistoricMeasurementsChartViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DateTimePoint> _observableValues;

        public ObservableCollection<ISeries> Series { get; set; }

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

        public HistoricMeasurementsChartViewModel()
        {
            _observableValues = new ObservableCollection<DateTimePoint>();

            var random = new Random();

            var datetime = DateTime.Now;
            var y = 0;
            for (int i = 0; i < 100; ++i)
            {
                _observableValues.Add(new DateTimePoint(datetime, y));
                datetime = datetime.AddMinutes(30);
                y += random.Next(-5, 5);
            }

            Series = new ObservableCollection<ISeries>()
            {
                new LineSeries<DateTimePoint>
                {
                    LineSmoothness = 0.3,
                    Values = _observableValues,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.LightBlue, 2),
                    GeometryStroke = new SolidColorPaint(SKColors.LightBlue, 2),
                    GeometrySize = 0,
                    TooltipLabelFormatter = (chartPoint) => $"{new DateTime((long) chartPoint.SecondaryValue):dd/MM HH:mm}: {chartPoint.PrimaryValue}",
                }
            };
        }
    }
}
