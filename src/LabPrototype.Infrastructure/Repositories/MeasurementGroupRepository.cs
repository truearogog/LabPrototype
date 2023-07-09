using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementGroupRepository : RepositoryBase<MeasurementGroupEntity>, IMeasurementGroupRepository
    {
        public MeasurementGroupRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementGroups)
        {
        }
    }
}
