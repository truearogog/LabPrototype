using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LabPrototype.DependencyInjection;
using LabPrototype.ViewModels.Main;
using LabPrototype.Views;
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
                DataContext = GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = DataContext
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static T GetRequiredService<T>() => Locator.Current.GetRequiredService<T>();
    }
}