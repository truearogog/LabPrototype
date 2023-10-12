using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;

namespace LabPrototype.Infrastructure.Repositories
{
    public class MeasurementGroupRepository : RepositoryBase<MeasurementGroupEntity>, IMeasurementGroupRepository
    {
        public MeasurementGroupRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementGroups)
        {
        }
    }
}
