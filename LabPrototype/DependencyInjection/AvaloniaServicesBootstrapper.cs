using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class AvaloniaServicesBootstrapper
    {
        public static void RegisterAvaloniaServices(IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<IMainWindowProvider>(() => new MainWindowProvider());
        }
    }
}
