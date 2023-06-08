using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeasurementTypeRepository : RepositoryBase<MeasurementTypeEntity>, IMeasurementTypeRepository
    {
        public MeasurementTypeRepository(LabDbContext dbContext, DbSet<MeasurementTypeEntity> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}
