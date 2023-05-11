using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class DependencyBootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            ConfigurationBootstrapper.RegisterConfiguration(services, resolver);
            DatabaseBootstrapper.RegisterDatabase(services, resolver);
            LoggingBootstrapper.RegisterLogging(services, resolver);
            ServiceBootstrapper.RegisterServices(services, resolver);
        }
    }
}
