using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;
using LabPrototype.Infrastructure.DataAccessLayer.Repositories;

namespace LabPrototype.Infrastructure.Repositories
{
    public class MeasurementGroupArchiveRepository : RepositoryBase<MeasurementGroupArchiveEntity>, IMeasurementGroupArchiveRepository
    {
        public MeasurementGroupArchiveRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementGroupArchives)
        {
        }
    }
}
