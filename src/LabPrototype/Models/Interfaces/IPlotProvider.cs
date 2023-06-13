using System;
using System.Drawing;

namespace LabPrototype.Models.Interfaces
{
    public interface IPlotProvider
    {
        void AddPlots(int[] plotIds, double[] xs, double[][] ys, Color[] colors);
        void ClearPlots();
        void SetPlotVisibility(int plotId, bool visible);
        public bool LockXAxis { get; set; }
        public bool LockYAxis { get; set; }

        void AddCrosshair();
        void ShowCrosshair();
        void HideCrosshair();
        void SetCrosshairPosition(double x, double y);

        (double, double, int) GetPointNearestX(double x);
    }
}
