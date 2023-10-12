using AutoMapper;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Framework.Helpers;
using LabPrototype.Framework.Models;
using LabPrototype.ViewModels.Forms;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;

namespace LabPrototype.ViewModels.Components.Settings
{
    public class MeterFormBaseViewModel : ViewModelBase
    {
        protected readonly IMapper Mapper;
        private readonly IMeterStore _meterStore;

        private Meter _model = new();
        public Meter Model
        {
            get => _model;
            set
            {
                this.RaiseAndSetIfChanged(ref _model, value);
                OnModelSet();
            }
        }

        private MeterFormViewModel _form = new();
        public MeterFormViewModel Form
        {
            get => _form;
            set => this.RaiseAndSetIfChanged(ref _form, value);
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

        public MeterFormBaseViewModel()
        {
            Mapper = GetRequiredService<IMapper>();
            _meterStore = GetRequiredService<IMeterStore>();

            var parities = EnumHelper.GetEnumModels<Parity>();
            CreateParities(parities);
            var stopBits = EnumHelper.GetEnumModels<StopBits>();
            CreateStopBits(stopBits);
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

        protected Meter? Submit(Func<IMeterStore, Meter, Meter?> submitAction)
        {
            if (Form is not null && !Form.Validate().Any())
            {
                _model = Mapper.Map<Meter>(_form);
                Model.PortName = Model.PortName.ToUpper();
                Model.Parity = Parities[SelectedParityIndex].Value;
                Model.StopBits = StopBits[SelectedStopBitsIndex].Value;
                return submitAction(_meterStore, Model);
            }
            return default;
        }

        protected virtual void OnModelSet()
        {
            Form = Mapper.Map<MeterFormViewModel>(_model);
            SelectedParityIndex = Parities.ToList().FindIndex(x => x.Value.Equals(Model.Parity));
            SelectedStopBitsIndex = StopBits.ToList().FindIndex(x => x.Value.Equals(Model.StopBits));
        }
    }
}
