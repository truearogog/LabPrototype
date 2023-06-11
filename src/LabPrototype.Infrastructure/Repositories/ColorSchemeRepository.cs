using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class ColorSchemeRepository : RepositoryBase<ColorSchemeEntity>, IColorSchemeRepository
    {
        public ColorSchemeRepository(LabDbContext dbContext) : base(dbContext, dbContext.ColorSchemes)
        {
        }
    }
}
