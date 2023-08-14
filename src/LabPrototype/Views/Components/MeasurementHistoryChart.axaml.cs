using Avalonia.Controls;
using LabPrototype.ViewModels.Components;
using LabPrototype.Providers.PlotProvider;
using Avalonia.Input;
using System;
using Avalonia.Threading;
using System.Diagnostics;

namespace LabPrototype.Views.Components
{
    public partial class MeasurementHistoryChart : UserControl
    {
        private MeasurementHistoryChartViewModel? _vm;
        private readonly IPlotProvider _plotProvider;

        private bool _isPanning = false;
        private PointerPoint? _prevPanningPoint;

        private IDisposable _plotUpdateTimer;
        private bool _isSelecting = false;
        private Edge _selectionEdge = Edge.None;
        private Edge _plotEdge = Edge.None;
        private double _edgePanSpeed = 0.004;

        private double _doubleClickMilliseconds = 300;
        private DateTime? _prevClickDateTime;
        private PointerPoint? _prevClickPoint;

        public MeasurementHistoryChart()
        {
            InitializeComponent();

            _plotProvider = new ScottPlotProvider(ChartControl);

            ChartControl.PointerMoved += _PointerMoved;
            ChartControl.PointerEnter += _PointerEnter;
            ChartControl.PointerLeave += _PointerLeave;
            ChartControl.PointerPressed += _PointerPressed;
            ChartControl.PointerReleased += _PointerReleased;

            _plotUpdateTimer = DispatcherTimer.Run(EdgePanTimer_Elapsed, TimeSpan.FromMilliseconds(14));

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

                _plotUpdateTimer.Dispose();
            };
        }

        private bool EdgePanTimer_Elapsed()
        {
            if (_plotEdge != Edge.None && _plotProvider.HasSelection && (_isSelecting || _selectionEdge != Edge.None))
            {
                var direction = _plotEdge switch
                {
                    Edge.Left => -1,
                    Edge.Right => 1,
                    _ => 0
                };
                _plotProvider.AxisPan(direction * _edgePanSpeed, 0);

                if (_isSelecting)
                {
                    _plotProvider.PanSelection();
                }
                else
                {
                    _selectionEdge = _plotProvider.PanSelectionEdge(_selectionEdge);
                }

                _plotProvider.LowQuality = true;
                ChartControl.Refresh();
            }
            else
            {
                _plotProvider.LowQuality = false;
            }

            return true;
        }

        private void _PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var plotPoint = e.GetCurrentPoint(ChartControl);
            var doubleClick = _prevClickDateTime.HasValue && (DateTime.UtcNow - _prevClickDateTime.Value).TotalMilliseconds < _doubleClickMilliseconds;
            if (doubleClick && _prevClickPoint is not null)
            {
                if (_prevClickPoint.Properties.IsLeftButtonPressed && plotPoint.Properties.IsLeftButtonPressed)
                {
                    _plotProvider.RemoveSelection();
                    _vm?.UpdateSelectionEdges(_plotProvider.SelectionMin, _plotProvider.SelectionMax);
                }
                _prevClickDateTime = null;
            }
            else
            {
                if (plotPoint.Properties.IsLeftButtonPressed)
                {
                    if (!_plotProvider.HasSelection)
                    {
                        _plotProvider.StartSelection();
                        _isSelecting = true;
                    }
                    else
                    {
                        _selectionEdge = _plotProvider.GetSelectionEdge();
                    }
                }
                if (plotPoint.Properties.IsMiddleButtonPressed)
                {
                    _isPanning = true;
                    _plotProvider.HideCrosshair();
                    Cursor = new Cursor(StandardCursorType.Hand);
                }
                if (plotPoint.Properties.IsRightButtonPressed)
                {
                    _plotProvider.LockXAxis = true;
                    _plotProvider.LockYAxis = false;
                }
                _prevClickDateTime = DateTime.UtcNow;
            }
            _prevClickPoint = plotPoint;
        }

        private void _PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                _isSelecting = false;
                _plotEdge = Edge.None;
                _selectionEdge = Edge.None;

                if (_plotProvider.HasSelection)
                {
                    _vm?.UpdateSelectionEdges(_plotProvider.SelectionMin, _plotProvider.SelectionMax);
                }
            }
            if (e.InitialPressMouseButton == MouseButton.Middle)
            {
                _isPanning = false;
                _prevPanningPoint = null;
                _plotProvider.ShowCrosshair();
                Cursor = Cursor.Default;
            }
            if (e.InitialPressMouseButton == MouseButton.Right)
            {
                _plotProvider.LockXAxis = false;
                _plotProvider.LockYAxis = true;
            }
        }

        private void _PointerMoved(object? sender, PointerEventArgs e)
        {
            var (coordinateX, coordinateY) = ChartControl.GetMouseCoordinates();
            _plotProvider.SetCrosshairPosition(coordinateX, coordinateY);
            var plotPoint = e.GetCurrentPoint(ChartControl);

            if (_isPanning)
            {
                if (_prevPanningPoint != null)
                {
                    var dx = (plotPoint.Position.X - _prevPanningPoint.Position.X) / ChartControl.Bounds.Width;
                    var dy = (plotPoint.Position.Y - _prevPanningPoint.Position.Y) / ChartControl.Bounds.Height;
                    _plotProvider.AxisPan(-dx, dy);
                }
                _prevPanningPoint = plotPoint;
            }
            else
            {
                if (_plotProvider.HasSelection)
                {
                    if (_isSelecting)
                    {
                        _plotProvider.PanSelection();
                    }
                    else
                    {
                        if (_selectionEdge != Edge.None)
                        {
                            _selectionEdge = _plotProvider.PanSelectionEdge(_selectionEdge);
                        }
                    }

                    _plotEdge = _plotProvider.GetPlotEdge();
                    if (_plotProvider.GetSelectionEdge() != Edge.None)
                        Cursor = new Cursor(StandardCursorType.Hand);
                    else
                        Cursor = Cursor.Default;
                }

                var nearestIndex = _plotProvider.GetPointNearestX(coordinateX).index;
                _vm?.UpdateNearestMeasurementGroup(nearestIndex);
            }
        }

        private void _PointerEnter(object? sender, PointerEventArgs e)
        {
            _plotProvider.ShowCrosshair();
        }

        private void _PointerLeave(object? sender, PointerEventArgs e)
        {
            _plotProvider.HideCrosshair();
        }
    }
}
