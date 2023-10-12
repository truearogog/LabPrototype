using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Providers.FlowMeasurementGroupProvider;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementListingViewModel : ViewModelBase
    {
        private readonly IMeterService _meterService;
        private readonly IFlowMeasurementGroupProvider _flowMeasurementGroupProvider;

        private int _meterId;
        public ObservableCollection<FlowMeasurementListingItemViewModel> FlowMeasurementListingItems { get; set; } = new();

        public FlowMeasurementListingViewModel()
        {
            _meterService = GetRequiredService<IMeterService>();
            _flowMeasurementGroupProvider = GetRequiredService<IFlowMeasurementGroupProvider>();
        }

        public override void Dispose()
        {
            _flowMeasurementGroupProvider.MeasurementGroupUpdated -= _MeasurementGroupUpdated;
            _flowMeasurementGroupProvider.Stop(_meterId);

            base.Dispose();
        }

        private void _MeasurementGroupUpdated(DisplayMeasurementGroup measurementGroup)
        {
            if (measurementGroup is not null && measurementGroup.MeterId == _meterId)
            {
                foreach (var flowMeasurementListingItem in FlowMeasurementListingItems)
                {
                    flowMeasurementListingItem.Update(measurementGroup);
                }
            }
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                _meterId = meter.Id;
                CreateMeasurements(meter);

                if (_flowMeasurementGroupProvider.IsRunning(_meterId) == false)
                {
                    _flowMeasurementGroupProvider.MeasurementGroupUpdated += _MeasurementGroupUpdated;
                    _flowMeasurementGroupProvider.Start(_meterId);
                }
            }
        }

        private void CreateMeasurements(Meter meter)
        {
            FlowMeasurementListingItems.Clear();
            var measurementTypes = _meterService.GetMeasurementTypes(meter.Id);
            foreach (var measurementType in measurementTypes)
            {
                FlowMeasurementListingItems.Add(new FlowMeasurementListingItemViewModel(measurementType));
            }
        }
    }
}
