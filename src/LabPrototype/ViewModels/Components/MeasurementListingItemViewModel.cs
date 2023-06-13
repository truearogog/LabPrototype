using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
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

        public MeasurementListingItemViewModel(MeasurementType measurementType)
        {
            _measurementType = measurementType;
        }

        public void Update(MeasurementGroup measurementGroup)
        {
            Value = measurementGroup.Measurements?.First(x => x.MeasurementTypeId.Equals(_measurementType.Id)).Value.ToString() ?? string.Empty;
            this.RaisePropertyChanged(nameof(HasValue));
        }
    }
}
