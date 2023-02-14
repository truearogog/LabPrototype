using LabPrototype.Domain.Models;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.MainWindow
{
    public class MeterListingViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IMeterService _meterStore;
        private readonly ISelectedMeterService _selectedMeterStore;

        public ICommand OpenCreateMeterCommand { get; }

        public ObservableCollection<MeterListingItemViewModel> MeterListingItemViewModels { get; } = new();

        private MeterListingItemViewModel _selectedMeterListingItemViewModel;
        public MeterListingItemViewModel SelectedMeterListingItemViewModel
        {
            get => _selectedMeterListingItemViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMeterListingItemViewModel, value);
                _selectedMeterStore.SelectedMeter = _selectedMeterListingItemViewModel?.Meter;
            }
        }

        public MeterListingViewModel(IDialogService dialogService, IMeterService meterStore, ISelectedMeterService selectedMeterStore)
        {
            _dialogService = dialogService;
            _meterStore = meterStore;
            _selectedMeterStore = selectedMeterStore;

            _meterStore.SubscribeMetersLoaded(MeterStore_MetersLoaded);

            OpenCreateMeterCommand = ReactiveCommand.CreateFromTask(ShowCreateMeterDialogAsync);
        }

        private Task ShowCreateMeterDialogAsync() => _dialogService.ShowDialogAsync(nameof(CreateMeterDialogViewModel));

        private void MeterStore_MetersLoaded()
        {
            MeterListingItemViewModels.Clear();

            foreach (var meter in _meterStore.Meters)
            {
                AddMeter(meter);
            }
        }

        private void AddMeter(Meter meter)
        {
            MeterListingItemViewModel meterListingItemViewModel = new MeterListingItemViewModel(meter);
            MeterListingItemViewModels.Add(meterListingItemViewModel);
        }
    }
}
