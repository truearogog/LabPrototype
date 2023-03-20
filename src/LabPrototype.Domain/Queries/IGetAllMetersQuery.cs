using LabPrototype.Domain.Models;

namespace LabPrototype.Domain.Queries
{
    public interface IGetAllMetersQuery
    {
        Task<IEnumerable<Meter>> Execute();
    }
}
