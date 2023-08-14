using System.Drawing;

namespace LabPrototype.Providers.PlotProvider
{
    public enum Edge
    {
        None,
        Left,
        Right
    }

    public interface IPlotProvider
    {
        void AddPlot(int plotId, double[] xs, double[] ys, Color color);
        void ClearPlots();
        void SetPlotVisibility(int plotId, bool visible);
        bool LockXAxis { get; set; }
        bool LockYAxis { get; set; }
        bool LowQuality { get; set; }

        void AddCrosshair();
        void ShowCrosshair();
        void HideCrosshair();
        void SetCrosshairPosition(double x, double y);

        Edge GetPlotEdge();
        Edge GetSelectionEdge();
        void StartSelection();
        void PanSelection();
        Edge PanSelectionEdge(Edge edge);
        void RemoveSelection();
        bool HasSelection { get; }
        double? SelectionMin { get; }
        double? SelectionMax { get; }

        void AxisPan(double dx, double dy);

        (double x, double y, int index) GetPointNearestX(double x);
    }
}
