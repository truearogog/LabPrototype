using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public interface IEntityTypeConfigurationBase
    {
        void Configure(ModelBuilder builder);
    }
}
