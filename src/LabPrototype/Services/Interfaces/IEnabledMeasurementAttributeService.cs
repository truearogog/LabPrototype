using System;

namespace LabPrototype.Services.Interfaces
{
    public interface IEnabledMeasurementAttributeService
    {
        event Action<Guid, bool> AttributeEnabledChanged;

        void Update(Guid attributeId, bool enabled);
        void Clear();
    }
}
