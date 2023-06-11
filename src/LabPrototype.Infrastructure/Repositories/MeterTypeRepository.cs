using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterTypeRepository : RepositoryBase<MeterTypeEntity>, IMeterTypeRepository
    {
        public MeterTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeterTypes)
        {
        }
    }
}
