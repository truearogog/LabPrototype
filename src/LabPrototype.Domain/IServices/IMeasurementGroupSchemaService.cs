using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IServices
{
    public interface IMeasurementGroupSchemaService : IServiceBase<MeasurementGroupSchema>
    {
        public IEnumerable<MeasurementType> GetMeasurementTypes(int id);
    }
}
