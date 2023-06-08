using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementGroupRepository : RepositoryBase<MeasurementGroupEntity>, IMeasurementGroupRepository
    {
        public MeasurementGroupRepository(LabDbContext dbContext, DbSet<MeasurementGroupEntity> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}
