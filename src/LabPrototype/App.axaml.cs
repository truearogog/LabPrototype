using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LabPrototype.Framework.Extensions;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Main;
using LabPrototype.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Splat;

namespace LabPrototype
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var windowService = GetRequiredService<IWindowService>();
                var mainWindow = windowService.ShowWindow<MainWindow, MainWindowViewModel>();
                desktop.MainWindow = mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static T GetRequiredService<T>() => Locator.Current.GetRequiredService<T>();
    }
}