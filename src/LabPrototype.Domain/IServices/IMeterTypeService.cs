using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IServices
{
    public interface IMeterTypeService : IServiceBase<MeterType>
    {
        IEnumerable<MeasurementType> GetMeasurementTypes(int id);
    }
}
