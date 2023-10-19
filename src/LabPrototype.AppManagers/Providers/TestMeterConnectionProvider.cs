using LabPrototype.Domain.IProviders;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Framework.Core;

namespace LabPrototype.AppManagers.Providers
{
    public class TestMeterConnectionProvider : IMeterConnectionProvider
    {
        public IEnumerable<MeasurementType> GetMeasurementTypes()
        {
            var colorSchemes = ColorScheme.All;
            return new MeasurementType[] { 
                new()
                {
                    Name = "Q1", 
                    Unit = "m³/h", 
                    PrimaryColor = colorSchemes.ElementAt(0).PrimaryColor, 
                    SecondaryColor = colorSchemes.ElementAt(0).SecondaryColor 
                }
            };
        }
    }
}
