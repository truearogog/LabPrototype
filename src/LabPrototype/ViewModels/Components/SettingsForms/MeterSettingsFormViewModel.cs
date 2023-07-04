using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public MeterSettingsFormViewModel() : base()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();

            var meterTypes = _meterTypeService.GetAll();
            CreateMeterTypes(meterTypes);
        }

        private void CreateMeterTypes(IEnumerable<MeterType> meterTypes)
        {
            MeterTypes.Clear();
            foreach (var meterType in meterTypes)
            {
                MeterTypes.Add(meterType);
            }
        }

        public override void PrepareModel()
        {
            Model.MeterTypeId = MeterTypes[SelectedMeterTypeIndex].Id;
        }

        protected override void OnModelSet()
        {
            SelectedMeterTypeIndex = MeterTypes.ToList().FindIndex(x => x?.Id.Equals(Model?.MeterTypeId) ?? false);
        }
    }
}
