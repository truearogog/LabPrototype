using AutoMapper;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Splat;
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
using LabPrototype.Domain.IRepositories;
using LabPrototype.Infrastructure.DataAccessLayer.Repositories;
using LabPrototype.Domain.IServices;
using LabPrototype.AppManagers.Services;
using LabPrototype.AppManagers.Stores;
using LabPrototype.Domain.IStores;
using LabPrototype.AppManagers.Profiles;
using System.Collections.Generic;
using LabPrototype.Services.WindowService;
using LabPrototype.Models.Profiles;
using LabPrototype.Providers.FlowMeasurementGroupProvider;
using LabPrototype.Infrastructure.Repositories;
using LabPrototype.Domain.Models.Presentation;
using System.Linq;

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

                // seed measurement groups
                // Seed(1);
                
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        private static void Seed(int id)
        {
            var mgService = Locator.Current.GetRequiredService<IMeasurementGroupService>();
            var aService = Locator.Current.GetRequiredService<IMeasurementGroupArchiveService>();
            var metService = Locator.Current.GetRequiredService<IMeterTypeService>();
            var sService = Locator.Current.GetRequiredService<IMeasurementGroupSchemaService>();
            var random = new Random();

            var mts = metService.GetMeasurementTypes(id);
            var a = aService.GetAll(x => x.MeterId == id);
            var s = sService.GetAll(x => x.MeterTypeId == id).OrderByDescending(x => x.Created).First();
            foreach (var archive in a)
            {
                // create measurement groups
                var mgs = new List<MeasurementGroup>();
                var dateTime = DateTime.UtcNow;
                var _mts = new (double, double)[mts.Count()];
                for (var i = 0; i < 1000; ++i)
                {
                    var avg = new double[mts.Count()];
                    _mts.Select(x => x.Item1).ToArray().CopyTo(avg, 0);
                    var sum = new double[mts.Count()];
                    _mts.Select(x => x.Item2).ToArray().CopyTo(sum, 0);
                    for (var j = 0; j < _mts.Length; ++j)
                    {
                        _mts[j].Item1 += random.Next(-5, 6);
                        _mts[j].Item2 += random.Next(-5, 6);
                    }
                    var mg = new MeasurementGroup
                    {
                        MeasurementGroupArchiveId = archive.Id,
                        MeasurementGroupSchemaId = s.Id,
                        DateTime = archive.DiscretizationMonths == 0 ? dateTime.AddMinutes(archive.DiscretizationMinutes * i) : dateTime.AddMonths(archive.DiscretizationMonths * i),
                        AverageValues = avg,
                        SummaryValues = sum,
                    };
                    mgs.Add(mg);
                }
                mgService.CreateRange(mgs);
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
                var profileTypes = new List<Type> {
                    typeof(ColorScheme_Profile),
                    typeof(MeasurementGroup_Profile),
                    typeof(MeasurementGroupArchive_Profile),
                    typeof(MeasurementGroupSchema_Profile),
                    typeof(MeasurementGroupSchemaMeasurementType_Profile),
                    typeof(MeasurementType_Profile),
                    typeof(Meter_Profile),
                    typeof(MeterType_Profile),

                    typeof(ColorSchemeForm_Profile),
                    typeof(MeasurementTypeForm_Profile),
                    typeof(MeterForm_Profile),
                    typeof(MeterTypeForm_Profile),
                    typeof(MeasurementGroupSchemaMeasurementTypeForm_Profile),
                };
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var profileType in profileTypes)
                    {
                        cfg.AddProfile(profileType);
                    }
                });
                return new Mapper(config);
            });
        }

        private static void RegisterRepositories(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<IColorSchemeRepository>(() => 
                new ColorSchemeRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementGroupArchiveRepository>(() =>
                new MeasurementGroupArchiveRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementGroupRepository>(() => 
                new MeasurementGroupRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementTypeRepository>(() => 
                new MeasurementTypeRepository(ResolveDbContext(resolver)));
            services.Register<IMeterRepository>(() => 
                new MeterRepository(ResolveDbContext(resolver)));
            services.Register<IMeterTypeRepository>(() => 
                new MeterTypeRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementGroupSchemaRepository>(() => 
                new MeasurementGroupSchemaRepository(ResolveDbContext(resolver)));
            services.Register<IMeasurementGroupSchemaMeasurementTypeRepository>(() => 
                new MeasurementGroupSchemaMeasurementTypeRepository(ResolveDbContext(resolver)));
        }

        private static LabDbContext ResolveDbContext(IReadonlyDependencyResolver resolver) => resolver.GetRequiredService<LabDbContextFactory>().Create();

        private static void RegisterDataServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<IColorSchemeService>(() => 
                new ColorSchemeService(ResolveMapper(resolver), resolver.GetRequiredService<IColorSchemeRepository>()));
            services.Register<IMeasurementGroupService>(() => 
                new MeasurementGroupService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementGroupRepository>()));
            services.Register<IMeasurementGroupArchiveService>(() =>
                new MeasurementGroupArchiveService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementGroupArchiveRepository>()));
            services.Register<IMeasurementTypeService>(() => 
                new MeasurementTypeService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementTypeRepository>()));
            services.Register<IMeterService>(() => 
                new MeterService(ResolveMapper(resolver), resolver.GetRequiredService<IMeterRepository>()));
            services.Register<IMeterTypeService>(() => 
                new MeterTypeService(ResolveMapper(resolver), resolver.GetRequiredService<IMeterTypeRepository>()));
            services.Register<IMeasurementGroupSchemaService>(() => 
                new MeasurementGroupSchemaService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementGroupSchemaRepository>()));
            services.Register<IMeasurementGroupSchemaMeasurementTypeService>(() => 
                new MeasurementGroupSchemaMeasurementTypeService(ResolveMapper(resolver), resolver.GetRequiredService<IMeasurementGroupSchemaMeasurementTypeRepository>()));
        }

        private static IMapper ResolveMapper(IReadonlyDependencyResolver resolver) => resolver.GetRequiredService<IMapper>();
        
        private static void RegisterStores(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IColorSchemeStore>(() => new ColorSchemeStore());
            services.RegisterLazySingleton<IMeasurementGroupStore>(() => new MeasurementGroupStore());
            services.RegisterLazySingleton<IMeasurementGroupArchiveStore>(() => new MeasurementGroupArchiveStore());
            services.RegisterLazySingleton<IMeasurementTypeStore>(() => new MeasurementTypeStore());
            services.RegisterLazySingleton<IMeterStore>(() => new MeterStore());
            services.RegisterLazySingleton<IMeterTypeStore>(() => new MeterTypeStore());
            services.RegisterLazySingleton<IMeasurementGroupSchemaStore>(() => new MeasurementGroupSchemaStore());
            services.RegisterLazySingleton<IMeasurementGroupSchemaMeasurementTypeStore>(() => new MeasurementGroupSchemaMeasurementTypeStore());
        }

        private static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IWindowService>(() => new WindowService());
            services.RegisterLazySingleton<IFlowMeasurementGroupProvider>(() =>
                new TestFlowMeasurementGroupProvider(resolver.GetRequiredService<IMeterService>(), resolver.GetRequiredService<IMeterTypeService>()));
        }

        private static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}