using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;

namespace LabPrototype.Infrastructure.Repositories
{
    public class MeasurementTypeRepository : RepositoryBase<MeasurementTypeEntity>, IMeasurementTypeRepository
    {
        public MeasurementTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementTypes)
        {
        }
    }
}
