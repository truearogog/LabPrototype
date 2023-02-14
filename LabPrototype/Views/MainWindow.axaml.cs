using Avalonia;
using Avalonia.Controls;

namespace LabPrototype.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowOverlay() => OverlayGrid.ZIndex = 1000;

        public void HideOverlay() => OverlayGrid.ZIndex = -1;
    }
}