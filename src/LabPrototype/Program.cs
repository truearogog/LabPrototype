﻿using AutoMapper;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using LabPrototype.AppManagers.Services;
using LabPrototype.AppManagers.Stores;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Configurations;
using LabPrototype.Framework.Extensions;
using LabPrototype.Infrastructure.DataAccessLayer;
using LabPrototype.Infrastructure.Repositories;
using LabPrototype.IoC;
using LabPrototype.Providers.IntegrationCacheProvider;
using LabPrototype.Providers.MeasurementIntegrationCacheProvider;
using LabPrototype.Services.WindowService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Splat;
using System;
using System.Globalization;
using System.Threading;

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
                CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

                RegisterDependencies();
                SubscribeToDomainUnhandledEvents();

                // ensure that database is created
                var contextFactory = Locator.Current.GetRequiredService<LabDbContextFactory>();
                using (var context = contextFactory.Create())
                {
                    context.Database.EnsureCreated();
                }

                // Seed(1);
                
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
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
            RegisterRepositories(Locator.CurrentMutable, Locator.Current);
            RegisterDataServices(Locator.CurrentMutable, Locator.Current);
            RegisterStores(Locator.CurrentMutable, Locator.Current);
            RegisterServices(Locator.CurrentMutable, Locator.Current);
        }

        private static void RegisterConfigurations(IMutableDependencyResolver services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var loggingConfig = GetConfiguration<LoggingConfiguration>(configuration, "Logging");
            services.RegisterConstant(loggingConfig);

            var dataAccessConfig = GetConfiguration<DataAccessConfiguration>(configuration, "DataAccess");
            services.RegisterConstant(dataAccessConfig);
        }

        private static T? GetConfiguration<T>(IConfigurationRoot configuration, string sectionName) where T : ConfigurationBase
        {
            var config = configuration.GetSection(sectionName).Get<T>();
            return config;
        }

        private static void RegisterDataAccess(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() => {
                var databaseConfiguration = resolver.GetRequiredService<DataAccessConfiguration>();
                return new DbContextOptionsBuilder().UseSqlite(databaseConfiguration.ConnectionString).Options;
            });

            services.RegisterLazySingleton(() => new LabDbContextFactory(resolver.GetRequiredService<DbContextOptions>()));
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
            services.RegisterLazySingleton<IMapper>(() => new Mapper(AutoMapperConfiguration.Build()));
        }

        private static void RegisterRepositories(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<IArchiveRepository>(() => new ArchiveRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementGroupRepository>(() => new MeasurementGroupRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementTypeRepository>(() => new MeasurementTypeRepository(ResolveDbContext(resolver)));
            services.Register<IMeterRepository>(() => new MeterRepository(ResolveDbContext(resolver)));
        }

        private static LabDbContext ResolveDbContext(IReadonlyDependencyResolver resolver) => resolver.GetRequiredService<LabDbContextFactory>().Create();

        private static void RegisterDataServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<IMeasurementGroupService>(() => 
                new MeasurementGroupService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementGroupRepository>()));
            services.Register<IArchiveService>(() =>
                new ArchiveService(ResolveMapper(resolver), resolver.GetRequiredService<IArchiveRepository>()));
            services.Register<IMeasurementTypeService>(() => 
                new MeasurementTypeService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementTypeRepository>()));
            services.Register<IMeterService>(() => 
                new MeterService(ResolveMapper(resolver), resolver.GetRequiredService<IMeterRepository>()));
        }

        private static IMapper ResolveMapper(IReadonlyDependencyResolver resolver) => resolver.GetRequiredService<IMapper>();
        
        private static void RegisterStores(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IMeasurementGroupStore>(() => new MeasurementGroupStore());
            services.RegisterLazySingleton<IArchiveStore>(() => new ArchiveStore());
            services.RegisterLazySingleton<IMeasurementTypeStore>(() => new MeasurementTypeStore());
            services.RegisterLazySingleton<IMeterStore>(() => new MeterStore());
        }

        private static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IWindowService>(() => new WindowService());
            services.RegisterLazySingleton<IMeasurementCacheProvider>(() => new MeasurementCacheProvider());
        }

        private static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}