using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class ColorSchemeRepository : RepositoryBase<ColorSchemeEntity>, IColorSchemeRepository
    {
        public ColorSchemeRepository(LabDbContext dbContext) : base(dbContext, dbContext.ColorSchemes)
        {
        }
    }
}
