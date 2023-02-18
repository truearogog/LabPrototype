using LabPrototype.Domain.Models;
using System;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeasurementProvider : IDisposable
    {
        void SubscribeMeasurementUpdated(Action<Measurement> handler);
        void UnsubscribeMeasurementUpdated(Action<Measurement> handler);
    }
}
