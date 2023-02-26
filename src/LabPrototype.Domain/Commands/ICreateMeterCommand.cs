using LabPrototype.Domain.Models;

namespace LabPrototype.Domain.Commands
{
    public interface ICreateMeterCommand
    {
        Task Execute(Meter meter);
    }
}
