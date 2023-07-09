using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterRepository : RepositoryBase<MeterEntity>, IMeterRepository
    {
        public MeterRepository(LabDbContext dbContext) : base(dbContext, dbContext.Meters)
        {
        }
    }
}
