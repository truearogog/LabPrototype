using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterTypeMeasurementTypeRepository : RepositoryBase<MeterTypeMeasurementTypeEntity>, IMeterTypeMeasurementTypeRepository
    {
        public MeterTypeMeasurementTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeterTypeMeasurementTypes)
        {
        }
    }
}
