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
            IMeasurementProvider measurementProvider, 
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            _measurementProvider = measurementProvider;
            _measurementProvider.SubscribeMeasurementUpdated(MeasurementProvider_MeasurementUpdated);

            _enabledMeasurementAttributeService = enabledMeasurementAttributeService;

            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        private void CreateMeasurements(Meter meter)
        {
            Items.Clear();
            _enabledMeasurementAttributeService.Clear();
            if (meter != null)
            {
                var measurementAttributes = meter.MeasurementAttributes;

                foreach (var measurementAttribute in measurementAttributes)
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

        private void SelectedMeterService_SelectedMeterUpdated()
        {
            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        private void MeasurementProvider_MeasurementUpdated(Measurement measurement)
        {
            //UpdateMeasurements(measurement);
        }
    }
}
