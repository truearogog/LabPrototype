using ScottPlot;
using ScottPlot.Avalonia;
using ScottPlot.Control;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Image = ScottPlot.Plottable.Image;

namespace LabPrototype.Providers.PlotProvider
{
    public class ScottPlotProvider : IPlotProvider
    {
        private readonly AvaPlot _plot;
        private Dictionary<int, SignalPlotXY> _signalPlotsXY = new();

        private Crosshair? _crosshair;
        private double _selectionStart;
        private Image? _selectionImage;

        public bool HasSelection => _selectionImage is not null;
        private double? _selectionMin => _selectionImage?.X;
        private double? _selectionMax => _selectionImage?.X + _selectionImage?.WidthInAxisUnits;
        public double? SelectionMin => (_selectionMin.HasValue && _selectionMax.HasValue) ? Math.Min(_selectionMin.Value, _selectionMax.Value) : null;
        public double? SelectionMax => (_selectionMin.HasValue && _selectionMax.HasValue) ? Math.Max(_selectionMin.Value, _selectionMax.Value) : null;

        public bool LockXAxis
        {
            get => _plot.Configuration.LockHorizontalAxis;
            set => _plot.Configuration.LockHorizontalAxis = value;
        }

        public bool LockYAxis
        {
            get => _plot.Configuration.LockVerticalAxis;
            set => _plot.Configuration.LockVerticalAxis = value;
        }

        public bool LowQuality
        { 
            get => _plot.Configuration.Quality == QualityMode.Low;
            set => _plot.Configuration.Quality = value ? QualityMode.Low : QualityMode.High;
        }

        public ScottPlotProvider(AvaPlot plot)
        {
            _plot = plot;
            _plot.ContextMenu = null;
            _plot.Plot.Style(Style.Gray2);
            _plot.Plot.Style(Color.Transparent, Color.Transparent);
            _plot.Configuration.RightClickDragZoom = false;
            _plot.Configuration.LeftClickDragPan = false;
            _plot.Configuration.MiddleClickDragZoom = false;
            _plot.Configuration.DoubleClickBenchmark = false;
            _plot.Plot.XAxis.DateTimeFormat(true);
            LockYAxis = true;
        }

        public void AddPlot(int plotId, double[] xs, double[] ys, Color color)
        {
            var plot = _plot.Plot.AddSignalXY(xs, ys, color);
            plot.MarkerShape = MarkerShape.openCircle;
            plot.LineWidth = 2;
            _signalPlotsXY[plotId] = plot;

            _plot.Plot.SetOuterViewLimits();
            _plot.Plot.AxisAuto(0.1, 0.05);
            _plot.Plot.SetOuterViewLimits(_plot.Plot.XAxis.Dims.Min, _plot.Plot.XAxis.Dims.Max, _plot.Plot.YAxis.Dims.Min, _plot.Plot.YAxis.Dims.Max);
            _plot.Refresh();
        }

        public void ClearPlots()
        {
            _plot.Plot.Clear();
            _signalPlotsXY.Clear();
        }

        public void SetPlotVisibility(int plotId, bool visible)
        {
            if (_signalPlotsXY.ContainsKey(plotId))
            {
                _signalPlotsXY[plotId].IsVisible = visible;
                _plot.Refresh();
            }
        }

        public void AddCrosshair()
        {
            _crosshair = _plot.Plot.AddCrosshair(0, 0);
            _crosshair.VerticalLine.PositionFormatter = x => DateTime.FromOADate(x).ToString();
        }

        public void ShowCrosshair()
        {
            if (_crosshair is not null)
            {
                _crosshair.IsVisible = true;
                _plot.Refresh();
            }
        }

        public void HideCrosshair()
        {
            if (_crosshair is not null)
            {
                _crosshair.IsVisible = false;
                _plot.Refresh();
            }
        }

        public void SetCrosshairPosition(double x, double y)
        {
            if (_crosshair is not null)
            {
                (_crosshair.X, _crosshair.Y) = (x, y);
                _plot.Refresh();
            }
        }

        public Edge GetPlotEdge()
        {
            var x = _plot.GetMouseCoordinates().x;
            if (x < _plot.Plot.XAxis.Dims.Min)
                return Edge.Left;
            if (x > _plot.Plot.XAxis.Dims.Max)
                return Edge.Right;
            return Edge.None;
        }

        public Edge GetSelectionEdge()
        {
            var pixelX = _plot.GetMousePixel().x;
            var (minX, maxX) = (_plot.Plot.GetCoordinateX(pixelX - 5), _plot.Plot.GetCoordinateX(pixelX + 5));
            if (minX < _selectionMin && maxX > _selectionMin)
                return Edge.Left;
            if (minX < _selectionMax && maxX > _selectionMax)
                return Edge.Right;
            return Edge.None;
        }

        public void StartSelection()
        {
            var bmp = new Bitmap(1, 1);
            using var g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Red)), 0, 0, 1, 1);

            RemoveSelection();
            _selectionStart = Math.Clamp(_plot.GetMouseCoordinates().x, _plot.Plot.XAxis.Dims.Min, _plot.Plot.XAxis.Dims.Max);
            _selectionImage = _plot.Plot.AddImage(bmp, _selectionStart, 0, 0, 1, Alignment.MiddleLeft);
            _selectionImage.BorderColor = Color.Red;
            _selectionImage.BorderSize = 1;
            _selectionImage.HeightInAxisUnits = 5000;
        }

        public void PanSelection()
        {
            if (_selectionImage is not null)
            {
                var x = Math.Clamp(_plot.GetMouseCoordinates().x, _plot.Plot.XAxis.Dims.Min, _plot.Plot.XAxis.Dims.Max);
                (_selectionImage.X, _selectionImage.WidthInAxisUnits) = x < _selectionStart ? (x, _selectionStart - x) : (_selectionStart, x - _selectionStart);
            }
        }

        public Edge PanSelectionEdge(Edge edge)
        {
            if (_selectionImage is not null)
            {
                var x = Math.Clamp(_plot.GetMouseCoordinates().x, _plot.Plot.XAxis.Dims.Min, _plot.Plot.XAxis.Dims.Max);
                if (edge == Edge.Left)
                {
                    _selectionImage.WidthInAxisUnits += _selectionImage.X - x;
                    _selectionImage.X = x;
                }
                else if (edge == Edge.Right)
                {
                    _selectionImage.WidthInAxisUnits = x - _selectionImage.X;
                }
            }
            return edge;
        }

        public void RemoveSelection()
        {
            if (_selectionImage is not null)
            {
                _plot.Plot.Remove(_selectionImage);
                _selectionImage = null;
            }
        }

        public void AxisPan(double dx, double dy)
        {
            if (!LockXAxis)
            {
                var _dx = (_plot.Plot.XAxis.Dims.Max - _plot.Plot.XAxis.Dims.Min) * dx;
                _plot.Plot.AxisPan(_dx, 0);
            }
            if (!LockYAxis)
            {
                var _dy = (_plot.Plot.YAxis.Dims.Max - _plot.Plot.YAxis.Dims.Min) * dy;
                _plot.Plot.AxisPan(0, _dy);
            }
        }

        public (double x, double y, int index) GetPointNearestX(double x)
        {
            var plot = _signalPlotsXY.FirstOrDefault().Value;
            if (plot is null)
                return (0, 0, 0);
            else
                return _signalPlotsXY.First().Value.GetPointNearestX(x);
        }
    }
}
