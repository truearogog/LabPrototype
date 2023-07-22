using System;
using LabPrototype.Domain.Models.Presentation.MeasurementGroups;

namespace LabPrototype.Providers.FlowMeasurementGroupProvider
{
    public interface IFlowMeasurementGroupProvider
    {
        event Action<FlowMeasurementGroup> MeasurementGroupUpdated;
        void Start(int meterId);
        bool IsRunning(int meterId);
        void Stop(int meterId);
    }
}
