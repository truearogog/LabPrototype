using Avalonia.Controls;
using Avalonia.Input;
using LabPrototype.ViewModels.Components;
using System;
using ScottPlot;

namespace LabPrototype.Views.Components
{
    public partial class MeasurementChart : UserControl
    {
        private MeasurementChartViewModel? vm;

        private double? PressedX = null;

        public MeasurementChart()
        {
            InitializeComponent();

            DataContextChanged += (s, e) => vm = DataContext as MeasurementChartViewModel;

            ChartControl.Plot.Style(Style.Gray2);
            ChartControl.Plot.Style(figureBackground: System.Drawing.Color.Transparent);
            ChartControl.Plot.Style(dataBackground: System.Drawing.Color.Transparent);
        }

        private void _PointerMoved(object? sender, PointerEventArgs e)
        {
            if (PressedX.HasValue)
            {
                double currentX = Math.Clamp(e.GetPosition(this).X, 1, Bounds.Width);
                double minLimitPercentage = Bounds.Width / Math.Min(currentX, PressedX.Value);
                double maxLimitPercentage = Bounds.Width / Math.Max(currentX, PressedX.Value);

                vm?.UpdateSelectedLimitLabels(minLimitPercentage, maxLimitPercentage);
            }
        }

        private void _PointerEnter(object? sender, PointerEventArgs e)
        {
            vm?.ShowCrosshair();
        }

        private void _PointerLeave(object? sender, PointerEventArgs e)
        {
            vm?.HideCrosshair();
        }

        private void _PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var point = e.GetCurrentPoint(this);
            if (point.Properties.IsRightButtonPressed)
            {
                PressedX = point.Position.X;
                vm?.ShowSelectedLimitLabels();
                vm?.HideCrosshair();
            }
        }

        private void _PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (PressedX.HasValue)
            {
                vm?.HideSelectedLimitLabels();
                vm?.ShowCrosshair();
                PressedX = null;
            }
        }
    }
}
