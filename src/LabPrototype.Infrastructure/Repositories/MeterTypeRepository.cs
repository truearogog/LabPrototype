using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterTypeRepository : RepositoryBase<MeterTypeEntity>, IMeterTypeRepository
    {
        public MeterTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeterTypes)
        {
        }

        public IEnumerable<MeasurementTypeEntity> GetMeasurementTypes(int id)
        {
            var entity = GetById(id);
            if (entity is null)
                return Enumerable.Empty<MeasurementTypeEntity>();
            return entity.MeterTypeMeasurementTypes
                .OrderBy(x => x.SortOrder)
                .Select(x => x.MeasurementType)
                .OfType<MeasurementTypeEntity>();
        }
    }
}
