using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementListingViewModel : ViewModelBase
    {
        private IMeasurementProvider _measurementProvider;

        private Meter? _meter;
        public Meter? Meter
        {
            get => _meter;
            set
            {
                _meter = value;
            }
        }

        public ObservableCollection<MeasurementListingItemViewModel> Items { get; set; } = new();

        public FlowMeasurementListingViewModel()
        {
            _measurementProvider = GetRequiredService<IFlowMeasurementProvider>();
            _measurementProvider.MeasurementUpdated += _MeasurementUpdated;

            if (_measurementProvider is IFlowMeasurementProvider flowMeasurementProvider)
            {
                if (!flowMeasurementProvider.IsRunning)
                {
                    flowMeasurementProvider.Start();
                }
            }
        }

        public override void Dispose()
        {
            _measurementProvider.MeasurementUpdated -= _MeasurementUpdated;
            base.Dispose();
        }

        public void UpdateMeter(Meter? meter)
        {
            Meter = meter;
            CreateMeasurements(meter);
        }

        private void CreateMeasurements(Meter? meter)
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

        private void _MeasurementUpdated(Measurement measurement)
        {
            if (Meter != null)
            {
                if (measurement.MeterId.Equals(Meter.Id))
                {
                    foreach (var measurementViewModel in Items)
                    {
                        measurementViewModel.Update(measurement);
                    }
                }
            }
        }
    }
}
