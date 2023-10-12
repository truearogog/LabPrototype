using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;

namespace LabPrototype.Infrastructure.Repositories
{
    public class ArchiveRepository : RepositoryBase<ArchiveEntity>, IArchiveRepository
    {
        public ArchiveRepository(LabDbContext dbContext) : base(dbContext, dbContext.Archives)
        {
        }
    }
}
