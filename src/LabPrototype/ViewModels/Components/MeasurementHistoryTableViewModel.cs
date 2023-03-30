using LabPrototype.Domain.Models;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryTableViewModel : MeasurementHistoryViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IMeasurementService _measurementService;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }

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

            }
        }
    }
}
