using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeasurementService
    {
        IDictionary<Guid, List<Measurement>> LoadedMeasurements { get; }

        Task LoadMeter(Guid meterId);

        void SubscribeMeterMeasurementsLoaded(Action<Guid> handler);
        void UnsubscribeMeterMeasurementsLoaded(Action<Guid> handler);
    }
}
