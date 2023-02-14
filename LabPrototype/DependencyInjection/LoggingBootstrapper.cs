using LabPrototype.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class LoggingBootstrapper
    {
        public static void RegisterLogging(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() =>
            {
                var config = resolver.GetRequiredService<LoggingConfiguration>();
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Default", config.DefaultLogLevel)
                    .MinimumLevel.Override("Microsoft", config.MicrosoftLogLevel)
                    .WriteTo.Console()
                    .WriteTo.File(config.LogFileName, fileSizeLimitBytes: config.LimitBytes, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                return logger;
            });
        }
    }
}
