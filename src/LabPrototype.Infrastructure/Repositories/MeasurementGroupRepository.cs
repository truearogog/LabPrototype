using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementGroupRepository : RepositoryBase<MeasurementGroupEntity>, IMeasurementGroupRepository
    {
        public MeasurementGroupRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementGroups)
        {
        }
    }
}
