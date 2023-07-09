using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementRepository : RepositoryBase<MeasurementEntity>, IMeasurementRepository
    {
        public MeasurementRepository(LabDbContext dbContext) : base(dbContext, dbContext.Measurements)
        {
        }
    }
}
