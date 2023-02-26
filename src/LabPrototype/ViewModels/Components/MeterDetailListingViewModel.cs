using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailListingViewModel : ViewModelBase
    {
        private Meter _meter;
        public Meter Meter
        {
            get => _meter;
            set
            {
                this.RaiseAndSetIfChanged(ref _meter, value);
                UpdateDetails();
            }
        }

        private readonly ISelectedMeterService _selectedMeterService;

        public ObservableCollection<MeterDetailListingItemViewModel> Items { get; } = new();

        public MeterDetailListingViewModel(ISelectedMeterService selectedMeterService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            CreateDetails();
            UpdateDetails();
        }

        private void CreateDetails()
        {
            Items.Clear();
            Items.Add(new MeterDetailListingItemViewModel("Name", x => x?.Name));
            Items.Add(new MeterDetailListingItemViewModel("Serial code", x => x?.SerialCode));
            Items.Add(new MeterDetailListingItemViewModel("Address", x => x?.Address));
        }

        private void UpdateDetails()
        {
            foreach (var detailViewModel in Items)
            {
                detailViewModel.Update(Meter);
            }
        }

        private void SelectedMeterService_SelectedMeterUpdated()
        {
            Meter = _selectedMeterService.SelectedMeter;
        }
    }
}
