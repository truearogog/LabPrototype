using Splat;

namespace LabPrototype.IoC
{
    public static class MutableDependencyResolverExtensions
    {
        public static void Register<TService, IImplementation>(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
            where TService : class
            where IImplementation : class, TService
        {

        }

        public static void RegisterConstant<TService, IImplementation>(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
            where TService : class
            where IImplementation : class, TService
        {

        }

        public static void RegisterLazySingleton<TService, IImplementation>(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver) 
            where TService : class 
            where IImplementation : class, TService
        {

        }
    }
}
