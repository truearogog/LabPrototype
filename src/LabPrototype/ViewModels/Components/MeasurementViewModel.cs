using LabPrototype.Domain.Models;
using ReactiveUI;
using System;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set => this.RaiseAndSetIfChanged(ref _unit, value);
        }

        private Func<Measurement, string> _valueSetter;

        public MeasurementViewModel(string name, string unit, Func<Measurement, string> valueSetter)
        {
            _name = name;
            _unit = unit;
            _valueSetter = valueSetter;
        }

        public void Update(Measurement measurement)
        {
            Value = _valueSetter(measurement);
        }
    }
}
