using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System;

namespace LabPrototype.Services.Implementations
{
    public class SelectedMeterService : ISelectedMeterService
    {
        private readonly IMeterService _meterService;

        public event Action<Meter> SelectedMeterUpdated;

        private Meter _selectedMeter;
        public Meter SelectedMeter
        {
            get => _selectedMeter;
            set
            {
                _selectedMeter = value;
                SelectedMeterUpdated?.Invoke(_selectedMeter);
            }
        }

        public SelectedMeterService(IMeterService meterService)
        {
            _meterService = meterService;
            _meterService.MeterCreated += meter => SelectedMeter = meter;
            _meterService.MeterUpdated += meter => SelectedMeter = meter;
        }
    }
}
