using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer
{
    public class LabDbContextFactory 
    {
        private readonly DbContextOptions _options;

        public LabDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public LabDbContext Create()
        {
            return new LabDbContext(_options);
        }
    }
}
