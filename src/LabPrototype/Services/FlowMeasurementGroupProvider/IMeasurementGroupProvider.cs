using LabPrototype.Domain.Models.Presentation;
using System;

namespace LabPrototype.Services.FlowMeasurementGroupProvider
{
    public interface IMeasurementGroupProvider
    {
        event Action<MeasurementGroup> MeasurementGroupUpdated;
    }
}
