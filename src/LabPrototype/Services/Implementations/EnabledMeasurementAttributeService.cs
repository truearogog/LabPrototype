using LabPrototype.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace LabPrototype.Services.Implementations
{
    public class EnabledMeasurementAttributeService : IEnabledMeasurementAttributeService
    {
        private IDictionary<Guid, bool> _attributeEnabled = new Dictionary<Guid, bool>();

        private event Action<Guid, bool> _attributeEnabledChanged;

        public void Update(Guid attributeId, bool enabled)
        {
            _attributeEnabled[attributeId] = enabled;
            _attributeEnabledChanged?.Invoke(attributeId, enabled);
        }

        public void Clear()
        {
            _attributeEnabled.Clear();
        }

        public void SubscribeAttributeEnabledChanged(Action<Guid, bool> handler) => _attributeEnabledChanged += handler;

        public void UnsubscribeAttributeEnabledChanged(Action<Guid, bool> handler) => _attributeEnabledChanged -= handler;
    }
}
