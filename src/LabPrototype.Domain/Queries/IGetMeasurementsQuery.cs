using LabPrototype.Domain.Models;

namespace LabPrototype.Domain.Queries
{
    public interface IGetMeasurementsQuery
    {
        Task<IEnumerable<Measurement>> Execute(Guid meterId);
    }
}
