using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementTypeRepository : RepositoryBase<MeasurementTypeEntity>, IMeasurementTypeRepository
    {
        public MeasurementTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementTypes)
        {
        }
    }
}
