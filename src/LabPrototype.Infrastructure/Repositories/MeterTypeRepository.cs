using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterTypeRepository : RepositoryBase<MeterTypeEntity>, IMeterTypeRepository
    {
        public MeterTypeRepository(LabDbContext dbContext, DbSet<MeterTypeEntity> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}
