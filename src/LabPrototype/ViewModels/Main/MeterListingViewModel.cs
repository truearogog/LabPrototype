using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MeterListingViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IMeterService _meterService;
        private readonly ISelectedMeterService _selectedMeterService;

        public ICommand OpenCreateMeterCommand { get; }

        public ObservableCollection<MeterListingItemViewModel> MeterListingItemViewModels { get; } = new();

        private MeterListingItemViewModel _selectedMeterListingItemViewModel;
        public MeterListingItemViewModel SelectedMeterListingItemViewModel
        {
            get => _selectedMeterListingItemViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMeterListingItemViewModel, value);
                _selectedMeterService.SelectedMeter = _selectedMeterListingItemViewModel?.Meter;
            }
        }

        public MeterListingViewModel(IDialogService dialogService, IMeterService meterService, ISelectedMeterService selectedmeterService)
        {
            _dialogService = dialogService;
            _meterService = meterService;
            _selectedMeterService = selectedmeterService;

            _meterService.SubscribeMetersLoaded(MeterService_MetersLoaded);
            _meterService.SubscribeMeterCreated(MeterService_MeterCreated);
            _meterService.SubscribeMeterUpdated(MeterService_MeterUpdated);
            _meterService.SubscribeMeterDeleted(MeterService_MeterDeleted);

            OpenCreateMeterCommand = ReactiveCommand.CreateFromTask(ShowCreateMeterDialogAsync);
        }

        private async Task ShowCreateMeterDialogAsync() => await _dialogService.ShowDialogAsync(nameof(CreateMeterDialogViewModel));

        private void MeterService_MetersLoaded()
        {
            MeterListingItemViewModels.Clear();

            foreach (var meter in _meterService.Meters)
            {
                AddMeter(meter);
            }
        }

        private void MeterService_MeterCreated(Meter meter)
        {
            AddMeter(meter);
        }

        private void MeterService_MeterUpdated(Meter meter)
        {
            var meterViewModel = MeterListingItemViewModels.FirstOrDefault(x => x.Meter.Id.Equals(meter.Id));
            if (meterViewModel != null)
            {
                meterViewModel.Meter = meter;
            }
        }

        private void MeterService_MeterDeleted(Guid id)
        {
            var meterViewModel = MeterListingItemViewModels.FirstOrDefault(x => x.Meter.Id.Equals(id));
            if (meterViewModel != null)
            {
                MeterListingItemViewModels.Remove(meterViewModel);
                _selectedMeterService.SelectedMeter = null;
            }
        }

        private void AddMeter(Meter meter)
        {
            MeterListingItemViewModel meterListingItemViewModel = new MeterListingItemViewModel(meter);
            MeterListingItemViewModels.Add(meterListingItemViewModel);
        }
    }
}
