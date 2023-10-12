using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IProviders
{
    public interface IMeterConnectionProvider
    {
        IEnumerable<MeasurementType> GetMeasurementTypes();
    }
}
