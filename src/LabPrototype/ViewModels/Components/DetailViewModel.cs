using LabPrototype.Domain.Models;
using ReactiveUI;
using System;

namespace LabPrototype.ViewModels.Components
{
    public class DetailViewModel : ViewModelBase
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

        private Func<Meter, string> _detailValueSetter;

        public DetailViewModel(string detailName, Func<Meter, string> detailValueSetter)
        {
            Name = detailName;
            _detailValueSetter = detailValueSetter;
        }

        public void Update(Meter meter)
        {
            Value = _detailValueSetter(meter);
        }
    }
}
