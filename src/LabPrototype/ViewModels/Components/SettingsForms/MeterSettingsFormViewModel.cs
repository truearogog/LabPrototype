using DynamicData;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Framework.Helpers;
using LabPrototype.Framework.Models;
using LabPrototype.Models.Forms;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;

namespace LabPrototype.ViewModels.Components.ModelSettings
{
    public class MeterSettingsFormViewModel : SettingsFormViewModelBase<Meter, IMeterStore, MeterForm>
    {
        public ObservableCollection<MeterType> MeterTypes { get; set; } = new();
        private int _selectedMeterTypeIndex;
        public int SelectedMeterTypeIndex
        {
            get => _selectedMeterTypeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedMeterTypeIndex, value);
        }

        public ObservableCollection<EnumModel<Parity>> Parities { get; set; } = new();
        private int _selectedParityIndex;
        public int SelectedParityIndex
        {
            get => _selectedParityIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedParityIndex, value);
        }

        public ObservableCollection<EnumModel<StopBits>> StopBits { get; set; } = new();
        private int _selectedStopBitsIndex;
        public int SelectedStopBitsIndex
        {
            get => _selectedStopBitsIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedStopBitsIndex, value);
        }

        private readonly IMeterTypeService _meterTypeService;

        public MeterSettingsFormViewModel() : base()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();

            var meterTypes = _meterTypeService.GetAll();
            CreateMeterTypes(meterTypes);

            var parities = EnumHelper.GetEnumModels<Parity>();
            CreateParities(parities);

            var stopBits = EnumHelper.GetEnumModels<StopBits>();
            CreateStopBits(stopBits);
        }

        private void CreateMeterTypes(IEnumerable<MeterType> meterTypes)
        {
            MeterTypes.Clear();
            foreach (var meterType in meterTypes)
            {
                MeterTypes.Add(meterType);
            }
        }

        private void CreateParities(IEnumerable<EnumModel<Parity>> parities)
        {
            Parities.Clear();
            foreach (var parity in parities)
            {
                Parities.Add(parity);
            }
        }

        private void CreateStopBits(IEnumerable<EnumModel<StopBits>> stopBits)
        {
            StopBits.Clear();
            foreach (var stopBit in stopBits)
            {
                StopBits.Add(stopBit);
            }
        }

        public override void PrepareModel()
        {
            base.PrepareModel();
            Model.PortName = Model.PortName.ToUpper();
            Model.MeterTypeId = MeterTypes[SelectedMeterTypeIndex].Id;
            Model.Parity = Parities[SelectedParityIndex].Value;
            Model.StopBits = StopBits[SelectedStopBitsIndex].Value;
        }

        protected override void OnModelSet()
        {
            base.OnModelSet();
            SelectedMeterTypeIndex = MeterTypes.ToList().FindIndex(x => x.Id.Equals(Model.MeterTypeId));
            SelectedParityIndex = Parities.ToList().FindIndex(x => x.Value.Equals(Model.Parity));
            SelectedStopBitsIndex = StopBits.ToList().FindIndex(x => x.Value.Equals(Model.StopBits));
        }
    }
}
