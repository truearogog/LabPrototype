using AutoMapper;

namespace LabPrototype.IoC
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Build()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName!.StartsWith("LabPrototype"));
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var profiles = types.Where(type => type.IsAssignableTo(typeof(Profile)));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return config;
        }
    }
}
