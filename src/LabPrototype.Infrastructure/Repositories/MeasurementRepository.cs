using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementRepository : RepositoryBase<MeasurementEntity>, IMeasurementRepository
    {
        public MeasurementRepository(LabDbContext dbContext) : base(dbContext, dbContext.Measurements)
        {
        }
    }
}
