using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailFormViewModel : ViewModelBase
    {
        private Meter _meter = new Meter();
        public Meter Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        private int _selectedMeterTypeIndex;
        public int SelectedMeterTypeIndex
        {
            get => _selectedMeterTypeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedMeterTypeIndex, value);
        }

        public ObservableCollection<MeterTypeViewModel> MeterTypes { get; set; } = new();

        private readonly IMeterTypeStore _meterTypeStore;

        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        public MeterDetailFormViewModel(ICommand cancelCommand, Action<Meter> submitAction)
        {
            CancelCommand = cancelCommand;
            SubmitCommand = ReactiveCommand.Create(() => submitAction(Meter));

            _meterTypeStore = GetRequiredService<IMeterTypeStore>();
            _meterTypeStore.ModelsLoaded += _MeterTypesLoaded;
            Task.Run(_meterTypeStore.LoadAll);
        }

        public override void Dispose()
        {
            _meterTypeStore.ModelsLoaded -= _MeterTypesLoaded;

            base.Dispose();
        }

        private void _MeterTypesLoaded(IEnumerable<MeterType> meterTypes)
        {
            MeterTypes.Clear();
            foreach(var meterType in meterTypes)
            {
                MeterTypes.Add(new MeterTypeViewModel(meterType));
            }
            SelectedMeterTypeIndex = MeterTypes.ToList().FindIndex(x => x.MeterType?.Id.Equals(Meter?.Id) ?? false);
        }
    }
}
