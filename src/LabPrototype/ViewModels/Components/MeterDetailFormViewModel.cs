using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeterDetailFormViewModel : ViewModelBase
    {
        private Meter _meter = new();
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

        private readonly IMeterStore _meterStore;
        private readonly IMeterTypeService _meterTypeService;

        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        public MeterDetailFormViewModel(ICommand cancelCommand, Action<IMeterStore, Meter> submitAction)
        {
            _meterStore = GetRequiredService<IMeterStore>();
            _meterTypeService = GetRequiredService<IMeterTypeService>();

            var meterTypes = _meterTypeService.GetAll();
            CreateMeterTypes(meterTypes);

            CancelCommand = cancelCommand;
            SubmitCommand = ReactiveCommand.Create(() => {
                Meter.MeterTypeId = MeterTypes[SelectedMeterTypeIndex].MeterType.Id;
                submitAction(_meterStore, Meter);
            });
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        private void CreateMeterTypes(IEnumerable<MeterType> meterTypes)
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
