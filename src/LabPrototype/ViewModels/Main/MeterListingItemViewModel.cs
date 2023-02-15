using LabPrototype.Domain.Models;
using ReactiveUI;

namespace LabPrototype.ViewModels.Main
{
    public class MeterListingItemViewModel : ViewModelBase
    {
        private Meter _meter;
        public Meter Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        public MeterListingItemViewModel(Meter meter)
        {
            Meter = meter;
        }
    }
}
