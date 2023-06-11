using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterRepository : RepositoryBase<MeterEntity>, IMeterRepository
    {
        public MeterRepository(LabDbContext dbContext) : base(dbContext, dbContext.Meters)
        {
        }
    }
}
