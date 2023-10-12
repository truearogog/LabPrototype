using LabPrototype.Domain.IProviders;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Providers
{
    public class TestMeterConnectionProvider : IMeterConnectionProvider
    {
        public IEnumerable<MeasurementType> GetMeasurementTypes()
        {
            return new MeasurementType[] { new() { Name = "Q1", Unit = "m³/h", PrimaryColor =  } }
        }
    }
}
