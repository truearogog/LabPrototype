using Splat;

namespace LabPrototype.Framework.Extensions
{
    public static class MutableDependencyResolverExtensions
    {
        public static void Register<TService, IImplementation>(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
            where TService : class
            where IImplementation : class, TService
        {
            var ctor = typeof(IImplementation).GetConstructors().Single();
            var args = ctor.GetParameters();


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
