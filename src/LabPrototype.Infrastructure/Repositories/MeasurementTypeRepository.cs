using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementTypeRepository : RepositoryBase<MeasurementTypeEntity>, IMeasurementTypeRepository
    {
        public MeasurementTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementTypes)
        {
        }
    }
}
