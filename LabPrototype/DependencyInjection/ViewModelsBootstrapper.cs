using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.ViewModels.MainWindow;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class ViewModelBootstrapper
    {
        public static void RegisterViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new CreateMeterDialogViewModel());

            services.RegisterLazySingleton(() => new MainWindowViewModel(
                resolver.GetRequiredService<MainViewModel>()
            ));

            services.RegisterLazySingleton(() => MainViewModel.LoadViewModel(
                resolver.GetRequiredService<IDialogService>(),
                resolver.GetRequiredService<IMeterService>(),
                resolver.GetRequiredService<ISelectedMeterService>()
            ));
        }
    }
}
