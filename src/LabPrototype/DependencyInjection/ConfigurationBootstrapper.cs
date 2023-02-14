using LabPrototype.Configuration;
using LabPrototype.EntityFramework.Configuration;
using Microsoft.Extensions.Configuration;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class ConfigurationBootstrapper
    {
        public static void RegisterConfiguration(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            var configuration = BuildConfiguration();

            RegisterDatabaseConfiguration(services, configuration);
            RegisterLoggingConfiguration(services, configuration);
        }

        private static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        private static void RegisterDatabaseConfiguration(IMutableDependencyResolver services, IConfiguration configuration)
        {
            var config = new DatabaseConfiguration();
            configuration.GetSection("Database").Bind(config);
            services.RegisterConstant(config);
        }

        private static void RegisterLoggingConfiguration(IMutableDependencyResolver services, IConfiguration configuration)
        {
            var config = new LoggingConfiguration();
            configuration.GetSection("Logging").Bind(config);
            services.RegisterConstant(config);
        }
    }
}
