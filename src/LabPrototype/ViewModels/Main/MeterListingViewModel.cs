using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
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

        public ObservableCollection<MeterListingItemViewModel> Items { get; } = new();

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

        public MeterListingViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedmeterService)
        {
            _dialogService = dialogService;

            _meterService = meterService;
            _meterService.MetersLoaded += _MetersLoaded;
            _meterService.MeterCreated += _MeterCreated;
            _meterService.MeterUpdated += _MeterUpdated;
            _meterService.MeterDeleted += _MeterDeleted;

            _selectedMeterService = selectedmeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            OpenCreateMeterCommand = ReactiveCommand.CreateFromTask(ShowCreateMeterDialogAsync);
        }

        public override void Dispose()
        {
            _meterService.MetersLoaded -= _MetersLoaded;
            _meterService.MeterCreated -= _MeterCreated;
            _meterService.MeterUpdated -= _MeterUpdated;
            _meterService.MeterDeleted -= _MeterDeleted;
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            base.Dispose();
        }

        private async Task ShowCreateMeterDialogAsync() => await _dialogService.ShowDialogAsync(nameof(CreateMeterDialogViewModel));

        private void _MetersLoaded()
        {
            Items.Clear();

            foreach (var meter in _meterService.Meters)
            {
                AddMeter(meter);
            }
        }

        private void _MeterCreated(Meter meter)
        {
            AddMeter(meter);
        }

        private void _MeterUpdated(Meter meter)
        {
            var meterViewModel = Items.FirstOrDefault(x => x.Meter.Id.Equals(meter.Id));
            if (meterViewModel != null)
            {
                meterViewModel.Meter = meter;
            }
        }

        private void _MeterDeleted(Guid id)
        {
            var meterViewModel = Items.FirstOrDefault(x => x.Meter.Id.Equals(id));
            if (meterViewModel != null)
            {
                Items.Remove(meterViewModel);
                _selectedMeterService.SelectedMeter = null;
            }
        }

        private void AddMeter(Meter meter)
        {
            MeterListingItemViewModel meterListingItemViewModel = new MeterListingItemViewModel(meter);
            Items.Add(meterListingItemViewModel);
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            if (meter == null && SelectedMeterListingItemViewModel != null)
            {
                SelectedMeterListingItemViewModel = null;
            }
        }
    }
}
