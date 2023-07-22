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

        private readonly IMeterService _meterService;
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupArchiveService _measurementGroupArchiveService;
        private readonly IMeasurementGroupArchiveStore _measurementGroupArchiveStore;

        public MeterSettingsFormViewModel() : base()
        {
            _meterService = GetRequiredService<IMeterService>();
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementGroupArchiveService = GetRequiredService<IMeasurementGroupArchiveService>();
            _measurementGroupArchiveStore = GetRequiredService<IMeasurementGroupArchiveStore>();

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

        protected override void AfterSubmit(Meter? model)
        {
            base.AfterSubmit(model);

            if (model is not null)
            {
                var measurementGroupArchives = new[]
                {
                    new MeasurementGroupArchive
                    {
                        Name = "10 minutes",
                        DiscretizationMinutes = 10,
                        MinDiscretizationMinutes = 1,
                        MaxDescretizationMinutes = 10,
                        DiscretizationMonths = 0,
                        Order = 1,
                        IsActive = true,
                        IsEditable = true,
                        MeterId = model.Id,
                    },
                    new MeasurementGroupArchive
                    {
                        Name = "60 minutes",
                        DiscretizationMinutes = 60,
                        MinDiscretizationMinutes = 10,
                        MaxDescretizationMinutes = 60,
                        DiscretizationMonths = 0,
                        Order = 2,
                        IsActive = true,
                        IsEditable = true,
                        MeterId = model.Id,
                    },
                    new MeasurementGroupArchive
                    {
                        Name = "24 hours",
                        DiscretizationMinutes = 1440,
                        MinDiscretizationMinutes = 0,
                        MaxDescretizationMinutes = 0,
                        DiscretizationMonths = 0,
                        Order = 3,
                        IsActive = true,
                        IsEditable = false,
                        MeterId = model.Id,
                    },
                    new MeasurementGroupArchive
                    {
                        Name = "30 days",
                        DiscretizationMinutes = 0,
                        MinDiscretizationMinutes = 0,
                        MaxDescretizationMinutes = 0,
                        DiscretizationMonths = 1,
                        Order = 4,
                        IsActive = true,
                        IsEditable = false,
                        MeterId = model.Id,
                    },
                };

                foreach (var measurementGroupArchive in measurementGroupArchives)
                {
                    _measurementGroupArchiveStore.Create(_measurementGroupArchiveService, measurementGroupArchive);
                }
                _meterService.Update(model);
            }
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
