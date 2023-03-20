using LabPrototype.Domain.Models;

namespace LabPrototype.Services.Interfaces
{
    public interface IChartMeasurementProvider : IMeasurementProvider
    {
        abstract Measurement Measurement { get; set; }
    }
}
