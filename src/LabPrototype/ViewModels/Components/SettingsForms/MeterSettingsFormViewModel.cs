using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.ModelSettings
{
    public class MeterSettingsFormViewModel : SettingsFormViewModelBase<Meter, IMeterStore>
    {
        private int _selectedMeterTypeIndex;
        public int SelectedMeterTypeIndex
        {
            get => _selectedMeterTypeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedMeterTypeIndex, value);
        }

        public ObservableCollection<MeterType> MeterTypes { get; set; } = new();

        private readonly IMeterTypeService _meterTypeService;

        public MeterSettingsFormViewModel(ICommand cancelCommand, Action<IMeterStore, Meter> submitAction) : base(cancelCommand, submitAction)
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();

            var meterTypes = _meterTypeService.GetAll();
            CreateMeterTypes(meterTypes);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        private void CreateMeterTypes(IEnumerable<MeterType> meterTypes)
        {
            MeterTypes.Clear();
            foreach (var meterType in meterTypes)
            {
                MeterTypes.Add(meterType);
            }
        }

        protected override void OnSubmit()
        {
            Model.MeterTypeId = MeterTypes[SelectedMeterTypeIndex].Id;
        }

        protected override void OnModelSet()
        {
            SelectedMeterTypeIndex = MeterTypes.ToList().FindIndex(x => x?.Id.Equals(Model?.MeterTypeId) ?? false);
        }
    }
}
