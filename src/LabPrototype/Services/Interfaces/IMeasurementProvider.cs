using LabPrototype.Domain.Models;
using System;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeasurementProvider
    {
        public event Action<Measurement> MeasurementUpdated;
    }
}
