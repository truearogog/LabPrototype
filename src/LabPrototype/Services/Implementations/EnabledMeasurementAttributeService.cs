using LabPrototype.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace LabPrototype.Services.Implementations
{
    public class EnabledMeasurementAttributeService : IEnabledMeasurementAttributeService
    {
        private IDictionary<Guid, bool> _attributeEnabled = new Dictionary<Guid, bool>();

        public event Action<Guid, bool> AttributeEnabledChanged;

        public void Update(Guid attributeId, bool enabled)
        {
            _attributeEnabled[attributeId] = enabled;
            AttributeEnabledChanged?.Invoke(attributeId, enabled);
        }

        public void Clear()
        {
            _attributeEnabled.Clear();
        }
    }
}
