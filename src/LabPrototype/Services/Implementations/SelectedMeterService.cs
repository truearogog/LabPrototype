using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System;

namespace LabPrototype.Services.Implementations
{
    public class SelectedMeterService : ISelectedMeterService
    {
        private readonly IMeterService _meterStore;

        private Meter _selectedMeter;
        public Meter SelectedMeter
        {
            get => _selectedMeter;
            set
            {
                _selectedMeter = value;
                _selectedMeterChanged?.Invoke();
            }
        }

        private event Action _selectedMeterChanged;

        public SelectedMeterService(IMeterService meterStore)
        {
            _meterStore = meterStore;
            _meterStore.SubscribeMeterCreated((meter) => SelectedMeter = meter);
            _meterStore.SubscribeMeterUpdated((meter) => SelectedMeter = meter);
        }

        public void SubscribeSelectedMeterChanged(Action handler) => _selectedMeterChanged += handler;
        public void UnsubscribeSelectedMeterChanged(Action handler) => _selectedMeterChanged -= handler;
    }
}
