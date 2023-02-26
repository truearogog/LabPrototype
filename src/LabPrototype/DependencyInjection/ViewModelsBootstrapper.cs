using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.ViewModels.Main;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class ViewModelBootstrapper
    {
        public static void RegisterViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new CreateMeterDialogViewModel(
                resolver.GetRequiredService<IMeterService>()
            ));

            services.Register(() => new UpdateMeterDialogViewModel(
                resolver.GetRequiredService<IMeterService>()
            ));
            
            services.Register(() => new DeleteMeterDialogViewModel(
                resolver.GetRequiredService<IMeterService>()
            ));

            services.RegisterLazySingleton(() => new MainWindowViewModel(
                resolver.GetRequiredService<MainViewModel>()
            ));

            services.RegisterLazySingleton(() => MainViewModel.LoadViewModel(
                resolver.GetRequiredService<IDialogService>(),
                resolver.GetRequiredService<IMeterService>(),
                resolver.GetRequiredService<ISelectedMeterService>(),
                resolver.GetRequiredService<IFlowMeasurementProvider>(),
                resolver.GetRequiredService<IEnabledMeasurementAttributeService>()
            ));
        }
    }
}
