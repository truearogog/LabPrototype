using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailsViewModel : ViewModelBase
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

        public ObservableCollection<DetailViewModel> DetailViewModels { get; } = new();

        public MeterDetailsViewModel(ISelectedMeterService selectedMeterService)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            CreateDetails();
            UpdateDetails();
        }

        private void CreateDetails()
        {
            DetailViewModels.Clear();
            DetailViewModels.Add(new DetailViewModel("Name", x => x?.Name));
            DetailViewModels.Add(new DetailViewModel("Serial code", x => x?.SerialCode));
            DetailViewModels.Add(new DetailViewModel("Address", x => x?.Address));
        }

        private void UpdateDetails()
        {
            foreach (var detailViewModel in DetailViewModels)
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
