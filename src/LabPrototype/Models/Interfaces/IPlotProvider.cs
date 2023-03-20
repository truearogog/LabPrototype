using System;
using System.Drawing;

namespace LabPrototype.Models.Interfaces
{
    public interface IPlotProvider
    {
        void ClearPlots();
        void AddPlot(Guid plotId, double[] xs, double[] ys, Color color);
        void SetPlotVisibility(Guid plotId, bool visible);

        void AddCrosshair();
        void ShowCrosshair();
        void HideCrosshair();
        void SetCrosshairPosition(double x, double y);

        (double, double, int) GetPointNearestX(double x);
    }
}
