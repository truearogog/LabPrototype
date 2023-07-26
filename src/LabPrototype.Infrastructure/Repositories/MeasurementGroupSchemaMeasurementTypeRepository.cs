using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Infrastructure.DataAccessLayer;
using LabPrototype.Infrastructure.DataAccessLayer.Repositories;

namespace LabPrototype.Infrastructure.Repositories
{
    public class MeasurementGroupSchemaMeasurementTypeRepository : RepositoryBase<MeasurementGroupSchemaMeasurementTypeEntity>, IMeasurementGroupSchemaMeasurementTypeRepository
    {
        public MeasurementGroupSchemaMeasurementTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeasurementGroupSchemeMeasurementTypes)
        {
        }
    }
}
