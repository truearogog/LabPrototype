using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System;

namespace LabPrototype.Services.Implementations
{
    public class SelectedMeterService : ISelectedMeterService
    {
        private readonly IMeterService _meterService;

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

        public SelectedMeterService(IMeterService meterService)
        {
            _meterService = meterService;
            _meterService.SubscribeMeterCreated((meter) => SelectedMeter = meter);
            _meterService.SubscribeMeterUpdated((meter) => SelectedMeter = meter);
        }

        public void SubscribeSelectedMeterUpdated(Action handler) => _selectedMeterChanged += handler;
        public void UnsubscribeSelectedMeterUpdated(Action handler) => _selectedMeterChanged -= handler;
    }
}
