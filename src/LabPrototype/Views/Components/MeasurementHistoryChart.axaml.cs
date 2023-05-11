using Avalonia.Controls;
using LabPrototype.ViewModels.Components;
using LabPrototype.Models.Implementations;
using LabPrototype.Models.Interfaces;

namespace LabPrototype.Views.Components
{
    public partial class MeasurementHistoryChart : UserControl
    {
        private MeasurementHistoryChartViewModel? _vm;
        private IPlotProvider _plotProvider;

        public MeasurementHistoryChart()
        {
            InitializeComponent();

            _plotProvider = new ScottPlotProvider(ChartControl);

            ChartControl.PointerMoved += _PointerMoved;
            ChartControl.PointerEnter += _PointerEnter;
            ChartControl.PointerLeave += _PointerLeave;

            DataContextChanged += (s, e) =>
            {
                _vm = DataContext as MeasurementHistoryChartViewModel;
                if (_vm != null)
                {
                    _vm.PlotProvider = _plotProvider;
                }
            };

            DetachedFromVisualTree += (s, e) =>
            {
                ChartControl.PointerMoved -= _PointerMoved;
                ChartControl.PointerEnter -= _PointerEnter;
                ChartControl.PointerLeave -= _PointerLeave;
            };
        }

        private void _PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            (double coordinateX, double coordinateY) = ChartControl.GetMouseCoordinates();
            _plotProvider.SetCrosshairPosition(coordinateX, coordinateY);

            (double nearestX, double nearestY, int nearestIndex) = _plotProvider.GetPointNearestX(coordinateX);
            _vm?.UpdateNearestMeasurement(nearestIndex);
        }

        private void _PointerEnter(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            _plotProvider.ShowCrosshair();
        }

        private void _PointerLeave(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            _plotProvider.HideCrosshair();
        }
    }
}
