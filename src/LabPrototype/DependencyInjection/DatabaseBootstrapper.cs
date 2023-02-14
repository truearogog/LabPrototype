using LabPrototype.EntityFramework;
using LabPrototype.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;
using Splat;

namespace LabPrototype.DependencyInjection
{
    public static class DatabaseBootstrapper
    {
        public static void RegisterDatabase(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            var databaseConfiguration = resolver.GetRequiredService<DatabaseConfiguration>();

            services.RegisterLazySingleton<DbContextOptions>(() => 
                new DbContextOptionsBuilder().UseSqlite(databaseConfiguration.ConnectionString).Options
            );

            services.RegisterLazySingleton<LabDbContextFactory>(() => new LabDbContextFactory(
                resolver.GetRequiredService<DbContextOptions>()
            ));
        }
    }
}
