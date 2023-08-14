using LabPrototype.Domain.Models.Entities;
using System;
using System.Collections.Generic;

namespace LabPrototype.Models
{
    public readonly record struct MeasurementDisplayMode(string Name, Func<MeasurementGroupEntity, IEnumerable<double>> ValueSelector)
    {
    }
}
