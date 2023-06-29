﻿using ScottPlot;
using ScottPlot.Avalonia;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using LabPrototype.Models.Interfaces;

namespace LabPrototype.Models.Implementations
{
    public class ScottPlotProvider : IPlotProvider
    {
        private readonly AvaPlot _plot;
        private Dictionary<int, SignalPlotXY> _signalPlotsXY = new();

        private Crosshair? _crosshair;

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

        public ScottPlotProvider(AvaPlot plot)
        {
            _plot = plot;

            _plot.ContextMenu = null;

            _plot.Plot.Style(Style.Gray2);
            _plot.Plot.Style(Color.Transparent, Color.Transparent);



            _plot.Plot.XAxis.DateTimeFormat(true);
        }

        public void AddPlot(int plotId, double[] xs, double[] ys, Color color)
        {
            var plot = _plot.Plot.AddSignalXY(xs, ys, color);
            plot.MarkerShape = MarkerShape.openCircle;
            plot.LineWidth = 2;
            _signalPlotsXY[plotId] = plot;

            _plot.Plot.SetOuterViewLimits(xs.Min(), xs.Max());
            _plot.Plot.AxisAuto();
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

        public void ToggleLockXAxis()
        {
            _plot.Configuration.LockHorizontalAxis = !_plot.Configuration.LockHorizontalAxis;
        }

        public void ToggleLockYAxis()
        {
            _plot.Configuration.LockVerticalAxis = !_plot.Configuration.LockVerticalAxis;
        }

        public void AddCrosshair()
        {
            _crosshair = _plot.Plot.AddCrosshair(0, 0);
            _crosshair.VerticalLine.PositionFormatter = x => DateTime.FromOADate(x).ToString();
        }

        public void ShowCrosshair()
        {
            if (_crosshair != null)
            {
                _crosshair.IsVisible = true;
                _plot.Refresh();
            }
        }

        public void HideCrosshair()
        {
            if (_crosshair != null)
            {
                _crosshair.IsVisible = false;
                _plot.Refresh();
            }
        }

        public void SetCrosshairPosition(double x, double y)
        {
            if (_crosshair != null)
            {
                _crosshair.X = x;
                _crosshair.Y = y;
                _plot.Refresh();
            }
        }

        public (double, double, int) GetPointNearestX(double x)
        {
            var plot = _signalPlotsXY.FirstOrDefault().Value;
            if (plot == null)
                return (0, 0, 0);
            else
                return _signalPlotsXY.First().Value.GetPointNearestX(x);
        }
    }
}
