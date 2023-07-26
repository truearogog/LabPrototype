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

            var latestSchema = entity.MeasurementGroupSchemas
                .OrderByDescending(x => x.Created)
                .FirstOrDefault();

            return latestSchema?
                .MeasurementGroupSchemaMeasurementTypes
                .Select(x => x.MeasurementType)
                .OfType<MeasurementTypeEntity>() ?? Enumerable.Empty<MeasurementTypeEntity>();
        }
    }
}
