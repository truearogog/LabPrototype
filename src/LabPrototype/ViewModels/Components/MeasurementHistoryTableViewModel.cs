using DynamicData;
using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryTableViewModel : MeasurementHistoryViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IMeasurementService _measurementService;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;
        public event Action<Meter> SelectedMeterUpdated;

        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }

        public ObservableCollection<Measurement> Measurements { get; set; } = new();

        public MeasurementHistoryTableViewModel(
            ISelectedMeterService selectedMeterService,
            IMeasurementService measurementService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            _measurementService = measurementService;

            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel(selectedMeterService);
        }

        public override void Dispose()
        {
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            base.Dispose();
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            if (SelectedMeter != null)
            {
                SelectedMeterUpdated.Invoke(meter);
                Task.Run(() => _measurementService.LoadMeter(meter.Id)).Wait();
                var measurements = _measurementService.LoadedMeasurements[SelectedMeter.Id];
                Measurements = new(measurements);
                this.RaisePropertyChanged(nameof(Measurements));
            }
        }
    }
}
