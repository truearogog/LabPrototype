using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;

namespace LabPrototype.ViewModels.MainWindow
{
    public class SelectedMeterViewModel : ViewModelBase
    {
        private readonly ISelectedMeterService _selectedMeterStore;
        public Meter SelectedMeter => _selectedMeterStore.SelectedMeter;

        public MeterDetailsViewModel MeterDetailsViewModel { get; }

        public SelectedMeterViewModel(ISelectedMeterService selectedMeterStore)
        {
            MeterDetailsViewModel = new MeterDetailsViewModel();

            _selectedMeterStore = selectedMeterStore;
            _selectedMeterStore.SubscribeSelectedMeterChanged(SelectedMeterStore_SelectedMeterChanged);
        }

        private void SelectedMeterStore_SelectedMeterChanged()
        {
            this.RaisePropertyChanged(nameof(SelectedMeter));

            MeterDetailsViewModel.Meter = SelectedMeter;
        }
    }
}
