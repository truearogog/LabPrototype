using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterRepository : RepositoryBase<MeterEntity>, IMeterRepository
    {
        public MeterRepository(LabDbContext dbContext, DbSet<MeterEntity> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}
