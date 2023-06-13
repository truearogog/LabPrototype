using LabPrototype.Domain.Entities;

namespace LabPrototype.Domain.IRepositories
{
    public interface IMeterTypeRepository : IRepositoryBase<MeterTypeEntity>
    {
        IEnumerable<MeasurementTypeEntity> GetMeasurementTypes(int id);
    }
}
