using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.ViewModels.Models
{
    public class MeterNavigationParameter : NavigationParameterBase
    {
        public Meter? Meter { get; }

        public MeterNavigationParameter(Meter? meter)
        {
            Meter = meter;
        }
    }
}
