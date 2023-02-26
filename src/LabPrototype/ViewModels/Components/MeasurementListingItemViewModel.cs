using LabPrototype.Domain.Models;
using ReactiveUI;
using System;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementListingItemViewModel : ViewModelBase
    {
        private MeasurementAttribute _measurementAttribute;
        public MeasurementAttribute MeasurementAttribute
        {
            get => _measurementAttribute;
            set => this.RaiseAndSetIfChanged(ref _measurementAttribute, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public bool HasValue => !string.IsNullOrEmpty(Value);

        public MeasurementListingItemViewModel(MeasurementAttribute measurementAttribute)
        {
            _measurementAttribute = measurementAttribute;
        }

        public void Update(Measurement measurement)
        {
            Value = _measurementAttribute?.ValueGetter?.Invoke(measurement).ToString() ?? string.Empty;
            this.RaisePropertyChanged(nameof(HasValue));
        }
    }
}
