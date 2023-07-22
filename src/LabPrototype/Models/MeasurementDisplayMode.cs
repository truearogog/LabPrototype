using LabPrototype.Domain.Models.Presentation.Measurements;
using System;

namespace LabPrototype.Models
{
    public readonly record struct MeasurementDisplayMode(string Name, Func<Measurement, double> ValueSelector)
    {
    }
}
