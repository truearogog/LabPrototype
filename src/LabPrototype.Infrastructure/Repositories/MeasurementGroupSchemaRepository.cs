using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;
using LabPrototype.Infrastructure.DataAccessLayer.Repositories;

namespace LabPrototype.Infrastructure.Repositories
{
    public class MeasurementGroupSchemaRepository : RepositoryBase<MeasurementGroupSchemaEntity>, IMeasurementGroupSchemaRepository
    {
        public MeasurementGroupSchemaRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementGroupSchemas)
        {
        }

        public IEnumerable<MeasurementTypeEntity> GetMeasurementTypes(int id)
        {
            return GetMany(id, x => x.MeasurementGroupSchemaMeasurementTypes)
                .Select(x => x.MeasurementType)
                .OfType<MeasurementTypeEntity>();
        }
    }
}
