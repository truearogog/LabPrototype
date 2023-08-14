using LabPrototype.Domain.Models.Presentation;
using System;

namespace LabPrototype.Providers.FlowMeasurementGroupProvider
{
    public interface IFlowMeasurementGroupProvider
    {
        event Action<DisplayMeasurementGroup> MeasurementGroupUpdated;
        void Start(int meterId);
        bool IsRunning(int meterId);
        void Stop(int meterId);
    }
}
