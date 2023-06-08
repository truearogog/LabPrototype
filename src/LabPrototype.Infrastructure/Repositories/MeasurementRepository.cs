using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementRepository : RepositoryBase<MeasurementEntity>, IMeasurementRepository
    {
        public MeasurementRepository(LabDbContext dbContext, DbSet<MeasurementEntity> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}
