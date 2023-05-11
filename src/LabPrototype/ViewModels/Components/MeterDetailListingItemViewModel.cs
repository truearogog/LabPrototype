using LabPrototype.Domain.Models;
using ReactiveUI;
using System;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailListingItemViewModel : ViewModelBase
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _value = string.Empty;
        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private Func<Meter, string?> _detailValueSetter;

        public MeterDetailListingItemViewModel(string detailName, Func<Meter, string?> detailValueSetter)
        {
            Name = detailName;
            _detailValueSetter = detailValueSetter;
        }

        public void Update(Meter meter)
        {
            Value = _detailValueSetter(meter) ?? string.Empty;
        }
    }
}
