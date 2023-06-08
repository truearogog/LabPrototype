using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class ColorSchemeRepository : RepositoryBase<ColorSchemeEntity>, IColorSchemeRepository
    {
        public ColorSchemeRepository(LabDbContext dbContext, DbSet<ColorSchemeEntity> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}
