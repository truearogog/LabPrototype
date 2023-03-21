using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterService;
        private readonly IMeasurementProvider _measurementProvider;
        private readonly IEnabledMeasurementAttributeService _enabledMeasurementAttributeService;

        public ObservableCollection<MeasurementListingItemViewModel> Items { get; set; } = new();

        public ToggleMeasurementListingViewModel(
            ISelectedMeterService selectedMeterService, 
            IChartMeasurementProvider measurementProvider, 
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            _measurementProvider = measurementProvider;
            _measurementProvider.MeasurementUpdated += _MeasurementUpdated;

            _enabledMeasurementAttributeService = enabledMeasurementAttributeService;

            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        public override void Dispose()
        {
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            _measurementProvider.MeasurementUpdated -= _MeasurementUpdated;
            base.Dispose();
        }

        private void CreateMeasurements(Meter meter)
        {
            _enabledMeasurementAttributeService.Clear();
            Items.Clear();
            if (meter != null)
            {
                Items.Add(new ToggleMeasurementListingItemViewModel(
                    new MeasurementAttribute(
                        "Time", 
                        string.Empty, 
                        x => x.DateTime.ToString(), 
                        ColorScheme.Midnight))
                );
                foreach (var measurementAttribute in meter.MeasurementAttributes)
                {
                    Items.Add(new ToggleMeasurementListingItemViewModel(measurementAttribute, _enabledMeasurementAttributeService));
                }
            }
        }

        private void UpdateMeasurements(Measurement measurement)
        {
            foreach (var measurementViewModel in Items)
            {
                measurementViewModel.Update(measurement);
            }
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            CreateMeasurements(meter);
        }

        private void _MeasurementUpdated(Measurement measurement)
        {
            UpdateMeasurements(measurement);
        }
    }
}
