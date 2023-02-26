using System;
using System.Collections.Generic;

namespace LabPrototype.Services.Interfaces
{
    public interface IEnabledMeasurementAttributeService
    {
        void Update(Guid attributeId, bool enabled);
        void Clear();
        void SubscribeAttributeEnabledChanged(Action<Guid, bool> handler);
        void UnsubscribeAttributeEnabledChanged(Action<Guid, bool> handler);
    }
}
