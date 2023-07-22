using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Domain.Models.Presentation.MeasurementGroups;
using ReactiveUI;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementListingItemViewModel : ViewModelBase
    {
        private MeasurementType _measurementType;
        public MeasurementType MeasurementType
        {
            get => _measurementType;
            set => this.RaiseAndSetIfChanged(ref _measurementType, value);
        }

        private double _value;
        public double Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public FlowMeasurementListingItemViewModel(MeasurementType measurementType)
        {
            _measurementType = measurementType;
        }

        public void Update(FlowMeasurementGroup measurementGroup)
        {
            Value = measurementGroup.Measurements?.FirstOrDefault(x => x.MeasurementTypeId.Equals(_measurementType.Id))?.Value ?? 0d;
        }
    }
}
