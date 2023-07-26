using LabPrototype.Domain.Models.Entities;

namespace LabPrototype.Domain.IRepositories
{
    public interface IMeasurementGroupSchemaRepository : IRepositoryBase<MeasurementGroupSchemaEntity>
    {
        IEnumerable<MeasurementTypeEntity> GetMeasurementTypes(int id);
    }
}
