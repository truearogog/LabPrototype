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
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterUpdated);

            _measurementProvider = measurementProvider;
            _measurementProvider.SubscribeMeasurementUpdated(MeasurementUpdated);

            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        public override void Dispose()
        {
            _selectedMeterService.UnsubscribeSelectedMeterUpdated(SelectedMeterUpdated);
            _measurementProvider.UnsubscribeMeasurementUpdated(MeasurementUpdated);
            base.Dispose();
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

        private void SelectedMeterUpdated()
        {
            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        private void MeasurementUpdated(Measurement measurement)
        {
            UpdateMeasurements(measurement);
        }
    }
}
