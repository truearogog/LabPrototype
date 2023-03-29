using LabPrototype.Domain.Models;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Threading.Tasks;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementTableViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IMeasurementService _measurementService;

        private Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public MeasurementTableViewModel(
            ISelectedMeterService selectedMeterService,
            IMeasurementService measurementService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            _measurementService = measurementService;
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
