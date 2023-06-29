using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementListingItemViewModel : ViewModelBase
    {
        private MeasurementType _measurementType;
        public MeasurementType MeasurementType
        {
            get => _measurementType;
            set => this.RaiseAndSetIfChanged(ref _measurementType, value);
        }

        private string _value = string.Empty;
        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public bool HasValue => !string.IsNullOrEmpty(Value);

        private Func<MeasurementGroup, string> _valueGetter;

        public MeasurementListingItemViewModel(MeasurementType measurementType, Func<MeasurementGroup, string>? valueGetter = null)
        {
            _measurementType = measurementType;
            _valueGetter = valueGetter 
                ?? (measurementGroup => measurementGroup.Measurements?.FirstOrDefault(x => x.MeasurementTypeId.Equals(_measurementType.Id))?.Value.ToString() ?? string.Empty);
        }

        public void Update(MeasurementGroup measurementGroup)
        {
            Value = _valueGetter(measurementGroup);
            this.RaisePropertyChanged(nameof(HasValue));
        }
    }
}
