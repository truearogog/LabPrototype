using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Services.FlowMeasurementGroupProvider;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementListingViewModel : ViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IFlowMeasurementGroupProvider _flowMeasurementGroupProvider;

        private int _meterId;
        public ObservableCollection<MeasurementListingItemViewModel> MeasurementListingItemViewModels { get; set; } = new();

        public FlowMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _flowMeasurementGroupProvider = GetRequiredService<IFlowMeasurementGroupProvider>();
            _flowMeasurementGroupProvider.MeasurementGroupUpdated += _MeasurementGroupUpdated;

            if (_flowMeasurementGroupProvider.IsRunning == false)
            {
                _flowMeasurementGroupProvider.Start();
            }
        }

        public override void Dispose()
        {
            _flowMeasurementGroupProvider.MeasurementGroupUpdated -= _MeasurementGroupUpdated;

            base.Dispose();
        }

        private void _MeasurementGroupUpdated(MeasurementGroup measurementGroup)
        {
            if (measurementGroup is not null && measurementGroup.MeterId.Equals(_meterId))
            {
                foreach (var measurementListingItemViewModel in MeasurementListingItemViewModels)
                {
                    measurementListingItemViewModel.Update(measurementGroup);
                }
            }
        }

        private void CreateMeasurements(Meter meter)
        {
            MeasurementListingItemViewModels.Clear();
            var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
            foreach (var measurementType in measurementTypes)
            {
                MeasurementListingItemViewModels.Add(new MeasurementListingItemViewModel(measurementType));
            }
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                _meterId = meter.Id;
                CreateMeasurements(meter);
            }
        }
    }
}
