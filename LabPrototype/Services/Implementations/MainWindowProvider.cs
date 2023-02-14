using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using LabPrototype.Services.Interfaces;

namespace LabPrototype.Services.Implementations
{
    public class MainWindowProvider : IMainWindowProvider
    {
        public Window GetMainWindow()
        {
            var lifetime = (IClassicDesktopStyleApplicationLifetime) Application.Current.ApplicationLifetime;

            return lifetime.MainWindow;
        }
    }
}
