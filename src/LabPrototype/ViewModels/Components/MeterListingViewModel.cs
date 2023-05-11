using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.Views.Dialogs;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeterListingViewModel : ViewModelBase
    {
        private readonly WindowViewModelBase _parentWindow;
        private readonly IWindowService _windowService;
        private readonly IMeterService _meterService;

        private MeterListingItemViewModel? _selectedMeterListingItemViewModel;
        public MeterListingItemViewModel? SelectedMeterListingItemViewModel
        {
            get => _selectedMeterListingItemViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMeterListingItemViewModel, value);
                

            }
        }

        public ICommand OpenCreateMeterCommand { get; }

        public ObservableCollection<MeterListingItemViewModel> Items { get; } = new();

        public MeterListingViewModel(WindowViewModelBase parentWindow)
        {
            _parentWindow = parentWindow;

            _windowService = GetRequiredService<IWindowService>();
            _meterService = GetRequiredService<IMeterService>();

            _meterService.MetersLoaded += _MetersLoaded;
            _meterService.MeterCreated += _MeterCreated;
            _meterService.MeterUpdated += _MeterUpdated;
            _meterService.MeterDeleted += _MeterDeleted;

            OpenCreateMeterCommand = ReactiveCommand.CreateFromTask(ShowCreateMeterDialogAsync);
        }

        public override void Dispose()
        {
            _meterService.MetersLoaded -= _MetersLoaded;
            _meterService.MeterCreated -= _MeterCreated;
            _meterService.MeterUpdated -= _MeterUpdated;
            _meterService.MeterDeleted -= _MeterDeleted;

            base.Dispose();
        }

        private async Task ShowCreateMeterDialogAsync() => await _windowService.ShowDialogAsync<CreateMeterDialog, CreateMeterDialogViewModel>(_parentWindow);

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
            }
        }

        private void AddMeter(Meter meter)
        {
            MeterListingItemViewModel meterListingItemViewModel = new MeterListingItemViewModel(meter);
            Items.Add(meterListingItemViewModel);
        }
    }
}
