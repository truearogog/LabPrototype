using System.Drawing;

namespace LabPrototype.Providers.PlotProvider
{
    public interface IPlotProvider
    {
        void AddPlot(int plotId, double[] xs, double[] ys, Color color);
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
