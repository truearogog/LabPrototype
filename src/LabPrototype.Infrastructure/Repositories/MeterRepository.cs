using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;

namespace LabPrototype.Infrastructure.Repositories
{
    public class MeterRepository : RepositoryBase<MeterEntity>, IMeterRepository
    {
        public MeterRepository(LabDbContext dbContext) : base(dbContext, dbContext.Meters)
        {
        }
    }
}
