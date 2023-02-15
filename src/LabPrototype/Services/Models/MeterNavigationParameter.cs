using LabPrototype.Domain.Models;

namespace LabPrototype.Services.Models
{
    public class MeterNavigationParameter : NavigationParameterBase
    {
        public Meter Meter { get; }

        public MeterNavigationParameter(Meter meter)
        {
            Meter = meter;
        }
    }
}
