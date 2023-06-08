using AutoMapper;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Splat;
using System.Reflection;
using System.Linq;
using System;
using System.Threading;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using LabPrototype.Framework.Extensions;
using LabPrototype.Domain.Models.Configurations;
using LabPrototype.Infrastructure.DataAccessLayer;

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

                // set culture
                CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

                SubscribeToDomainUnhandledEvents();
                RegisterDependencies();

                // ensure that database is created
                var contextFactory = Locator.Current.GetRequiredService<LabDbContextFactory>();
                using (var context = contextFactory.Create())
                {
                    context.Database.EnsureCreated();
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

        private static void RegisterDependencies()
        {
            RegisterConfigurations(Locator.CurrentMutable);
            RegisterLogging(Locator.CurrentMutable, Locator.Current);
            RegisterDataAccess(Locator.CurrentMutable, Locator.Current);
            RegisterAutoMapper(Locator.CurrentMutable);
        }

        private static void RegisterConfigurations(IMutableDependencyResolver services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var loggingConfig = GetConfiguration<LoggingConfiguration>(configuration, "Logging");
            RegisterConfiguration(services, loggingConfig);

            var dataAccessConfig = GetConfiguration<DataAccessConfiguration>(configuration, "DataAccess");
            RegisterConfiguration(services, dataAccessConfig);
        }

        private static T? GetConfiguration<T>(IConfigurationRoot configuration, string sectionName) where T : ConfigurationBase
        {
            var config = configuration.GetSection(sectionName).Get<T>();
            return config;
        }

        private static void RegisterConfiguration<T>(IMutableDependencyResolver services, T? configuration) where T : ConfigurationBase
        {
            services.RegisterConstant(configuration);
        }

        private static void RegisterDataAccess(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            var databaseConfiguration = resolver.GetRequiredService<DataAccessConfiguration>();

            services.RegisterLazySingleton(() =>
                new DbContextOptionsBuilder().UseSqlite(databaseConfiguration.ConnectionString).Options
            );

            services.RegisterLazySingleton(() => new LabDbContextFactory(
                resolver.GetRequiredService<DbContextOptions>()
            ));
        }

        private static void RegisterLogging(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() =>
            {
                var config = resolver.GetRequiredService<LoggingConfiguration>();
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Default", config.DefaultLogLevel)
                    .MinimumLevel.Override("Microsoft", config.MicrosoftLogLevel)
                    .WriteTo.Console()
                    .WriteTo.File(
                        config.LogFileName ?? throw new Exception("No log file name!"), 
                        fileSizeLimitBytes: config.LimitBytes, 
                        rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                return logger;
            });
        }

        private static void RegisterAutoMapper(IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<IMapper>(() =>
            {
                var profileTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetType().Name.EndsWith("_Profile"));
                var config = new MapperConfiguration(cfg => {
                    foreach (var profileType in profileTypes)
                    {
                        cfg.AddProfile(profileType);
                    }
                });
                return new Mapper(config);
            });
        }

        private static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}