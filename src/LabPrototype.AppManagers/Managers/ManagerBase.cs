using AutoMapper;

namespace LabPrototype.AppManagers.Managers
{
    public class ManagerBase
    {
        public ManagerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper;

        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;
    }
}
