using Avalonia.Controls;
using LabPrototype.ViewModels.Components;
using LabPrototype.Models.Implementations;
using LabPrototype.Models.Interfaces;

namespace LabPrototype.Views.Components
{
    public partial class MeasurementHistoryChart : UserControl
    {
        private MeasurementHistoryChartViewModel? _vm;
        private readonly IPlotProvider _plotProvider;

        private bool _isPanning = false;

        public MeasurementHistoryChart()
        {
            InitializeComponent();

            _plotProvider = new ScottPlotProvider(ChartControl);

            ChartControl.PointerMoved += _PointerMoved;
            ChartControl.PointerEnter += _PointerEnter;
            ChartControl.PointerLeave += _PointerLeave;
            ChartControl.PointerPressed += _PointerPressed;
            ChartControl.PointerReleased += _PointerReleased;

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
                ChartControl.PointerPressed -= _PointerPressed;
                ChartControl.PointerReleased -= _PointerReleased;
            };
        }

        private void _PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {

        }

        private void _PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {

        }

        private void _PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            (double coordinateX, double coordinateY) = ChartControl.GetMouseCoordinates();
            _plotProvider.SetCrosshairPosition(coordinateX, coordinateY);

            (double nearestX, double nearestY, int nearestIndex) = _plotProvider.GetPointNearestX(coordinateX);
            _vm?.UpdateNearestMeasurementGroup(nearestIndex);
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
