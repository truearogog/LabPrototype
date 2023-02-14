using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Splat;
using System;
using System.Threading;
using LabPrototype.DependencyInjection;
using Serilog.Core;
using LabPrototype.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype
{
    internal class Program
    {
        private const int TimeoutSeconds = 3;

        [STAThread]
        public static void Main(string[] args)
        {
            var mutex = new Mutex(false, typeof(Program).FullName);

            try
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(TimeoutSeconds), true))
                {
                    return;
                }

                SubscribeToDomainUnhandledEvents();
                RegisterDependencies();

                // ensure that database is created
                var contextFactory = Locator.Current.GetRequiredService<LabDbContextFactory>();
                using (var context = contextFactory.Create())
                {
                    context.Database.Migrate();
                }

                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);

                // todo: get selected meter and save to database
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        private static void SubscribeToDomainUnhandledEvents() =>
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            var logger = Locator.Current.GetRequiredService<Logger>();
            var ex = (Exception)args.ExceptionObject;

            logger.Error($"Unhandled application error: {ex}");
        };

        private static void RegisterDependencies() =>
            Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);

        private static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }
}