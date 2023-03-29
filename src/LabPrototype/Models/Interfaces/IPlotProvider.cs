using System;
using System.Drawing;

namespace LabPrototype.Models.Interfaces
{
    public interface IPlotProvider
    {
        void AddPlots(Guid[] plotIds, double[] xs, double[][] ys, Color[] colors);
        void ClearPlots();
        void SetPlotVisibility(Guid plotId, bool visible);
        public bool LockXAxis { get; set; }
        public bool LockYAxis { get; set; }

        void AddCrosshair();
        void ShowCrosshair();
        void HideCrosshair();
        void SetCrosshairPosition(double x, double y);

        (double, double, int) GetPointNearestX(double x);
    }
}
