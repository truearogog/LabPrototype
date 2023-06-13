using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;

namespace LabPrototype.ViewModels.Components
{
    public class MeterTypeViewModel : ViewModelBase
    {
        private MeterType _meterType = new MeterType();
        public MeterType MeterType
        {
            get => _meterType;
            set => this.RaiseAndSetIfChanged(ref _meterType, value);
        }

        public MeterTypeViewModel(MeterType meterType)
        {
            MeterType = meterType;
        }
    }
}
