using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LabPrototype.EntityFramework
{
    public class LabDesignTimeDbContextFactory : IDesignTimeDbContextFactory<LabDbContext>
    {
        public LabDbContext CreateDbContext(string[] args)
        {
            return new LabDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=Lab.db").Options);
        }
    }
}
