using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementListingViewModel : ViewModelBase
    {
        private ISelectedMeterService _selectedMeterService;
        private IMeasurementProvider _measurementProvider;

        public ObservableCollection<MeasurementListingItemViewModel> Items { get; set; } = new();

        public FlowMeasurementListingViewModel(ISelectedMeterService selectedMeterService, IMeasurementProvider measurementProvider)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            _measurementProvider = measurementProvider;
            _measurementProvider.SubscribeMeasurementUpdated(MeasurementProvider_MeasurementUpdated);

            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        private void CreateMeasurements(Meter meter)
        {
            Items.Clear();
            if (meter != null)
            {
                var measurementAttributes = meter.MeasurementAttributes;

                foreach (var measurementAttribute in measurementAttributes)
                {
                    Items.Add(new MeasurementListingItemViewModel(measurementAttribute));
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
            UpdateMeasurements(measurement);
        }
    }
}
