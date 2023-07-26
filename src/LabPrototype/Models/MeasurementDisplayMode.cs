using LabPrototype.Domain.Models.Presentation;
using System;
using System.Collections.Generic;

namespace LabPrototype.Models
{
    public readonly record struct MeasurementDisplayMode(string Name, Func<MeasurementGroup, IEnumerable<double>> ValueSelector)
    {
    }
}
