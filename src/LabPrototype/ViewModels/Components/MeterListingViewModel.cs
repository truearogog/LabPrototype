using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.Views.Dialogs;
using ReactiveUI;
using System.Collections.Generic;
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
        private readonly IMeterStore _meterStore;

        public ICommand OpenCreateMeterCommand { get; }

        public ObservableCollection<MeterListingItemViewModel> Items { get; } = new();

        public MeterListingViewModel(WindowViewModelBase parentWindow)
        {
            _parentWindow = parentWindow;

            _windowService = GetRequiredService<IWindowService>();

            _meterStore = GetRequiredService<IMeterStore>();
            _meterStore.ModelsLoaded += _MetersLoaded;
            _meterStore.ModelCreated += _MeterCreated;
            _meterStore.ModelUpdated += _MeterUpdated;
            _meterStore.ModelDeleted += _MeterDeleted;
            Task.Run(_meterStore.LoadAll);

            OpenCreateMeterCommand = ReactiveCommand.CreateFromTask(ShowCreateMeterDialogAsync);
        }

        public override void Dispose()
        {
            _meterStore.ModelsLoaded -= _MetersLoaded;
            _meterStore.ModelCreated -= _MeterCreated;
            _meterStore.ModelUpdated -= _MeterUpdated;
            _meterStore.ModelDeleted -= _MeterDeleted;

            base.Dispose();
        }

        private async Task ShowCreateMeterDialogAsync() => await _windowService.ShowDialogAsync<CreateMeterDialog, CreateMeterDialogViewModel>(_parentWindow);

        private void _MetersLoaded(IEnumerable<Meter> meters)
        {
            Items.Clear();

            foreach (var meter in meters)
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
            if (meterViewModel is not null)
            {
                meterViewModel.Meter = meter;
            }
        }

        private void _MeterDeleted(int id)
        {
            var meterViewModel = Items.FirstOrDefault(x => x.Meter.Id.Equals(id));
            if (meterViewModel is not null)
            {
                Items.Remove(meterViewModel);
            }
        }

        private void AddMeter(Meter meter)
        {
            var meterListingItemViewModel = new MeterListingItemViewModel(meter);
            Items.Add(meterListingItemViewModel);
        }
    }
}
