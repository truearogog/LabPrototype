using LabPrototype.Domain.Commands;
using LabPrototype.Domain.Queries;
using LabPrototype.EntityFramework;
using LabPrototype.EntityFramework.Commands;
using LabPrototype.EntityFramework.Queries;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class ServiceBootstrapper
    {
        public static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterQueries(services, resolver);
            RegisterCommands(services, resolver);
            RegisterCommonServices(services, resolver);
        }

        private static void RegisterQueries(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<IGetAllMetersQuery>(() => new GetAllMetersQuery(
                resolver.GetRequiredService<LabDbContextFactory>()
            ));

            services.Register<IGetMeasurementsQuery>(() => new TestGetMeasurementsQuery(
                resolver.GetRequiredService<LabDbContextFactory>()
            ));
        }

        private static void RegisterCommands(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<ICreateMeterCommand>(() => new CreateMeterCommand(
                resolver.GetRequiredService<LabDbContextFactory>()
            ));

            services.Register<IUpdateMeterCommand>(() => new UpdateMeterCommand(
                resolver.GetRequiredService<LabDbContextFactory>()
            ));

            services.Register<IDeleteMeterCommand>(() => new DeleteMeterCommand(
                resolver.GetRequiredService<LabDbContextFactory>()
            ));
        }

        private static void RegisterCommonServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IDialogService>(() => new DialogService(
                resolver.GetRequiredService<IMainWindowProvider>()
            ));

            services.RegisterLazySingleton<IMeterService>(() => new MeterService(
                resolver.GetRequiredService<IGetAllMetersQuery>(),
                resolver.GetRequiredService<ICreateMeterCommand>(),
                resolver.GetRequiredService<IUpdateMeterCommand>(),
                resolver.GetRequiredService<IDeleteMeterCommand>()
            ));

            services.RegisterLazySingleton<IMeasurementService>(() => new MeasurementService(
                resolver.GetRequiredService<IGetMeasurementsQuery>()
            ));

            services.RegisterLazySingleton<ISelectedMeterService>(() => new SelectedMeterService(
                resolver.GetRequiredService<IMeterService>()
            ));

            services.RegisterLazySingleton<IFlowMeasurementProvider>(() => new TestFlowMeasurementProvider());

            services.RegisterLazySingleton<IChartMeasurementProvider>(() => new ChartMeasurementProvider());

            services.RegisterLazySingleton<IEnabledMeasurementAttributeService>(() => new EnabledMeasurementAttributeService());
        }
    }
}
