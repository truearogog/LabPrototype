using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeasurementService
    {
        event Action<Guid> MeterMeasurementsLoaded;

        Dictionary<Guid, List<Measurement>> LoadedMeasurements { get; }

        Task LoadMeter(Guid meterId);
    }
}
