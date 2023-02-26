using LabPrototype.Domain.Models;
using LabPrototype.Domain.Models.Meters;

namespace LabPrototype.Domain.Factories
{
    public static class MeterFactory
    {
        public static Meter CreateMeter(Guid id, string serialCode, string name, string address, int type)
        {
            foreach (var meterType in MeterType.All)
            {
                if (meterType.Id.Equals(type))
                {
                    return (Meter)Activator.CreateInstance(meterType.Type, id, serialCode, name, address);
                }
            }
            throw new ArgumentException("Unsupported meter type!");
        }
    }
}
