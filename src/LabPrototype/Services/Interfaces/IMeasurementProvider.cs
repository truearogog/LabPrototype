using LabPrototype.Domain.Models;
using System;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeasurementProvider
    {
        event Action<Measurement> MeasurementUpdated;
    }
}
