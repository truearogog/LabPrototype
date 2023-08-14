﻿using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.ModelSettings;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components.SettingsForms
{
    public class MeasurementGroupSchemaMeasurementTypeSettingsFormViewModel : SettingsFormViewModelBase<
        MeasurementGroupSchemaMeasurementType,
        IMeasurementGroupSchemaMeasurementTypeStore, 
        MeasurementGroupSchemaMeasurementTypeForm>
    {

        private int _selectedMeasurementTypeIndex;
        public int SelectedMeasurementTypeIndex
        {
            get => _selectedMeasurementTypeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedMeasurementTypeIndex, value);
        }

        public ObservableCollection<MeasurementType> MeasurementTypes { get; set; } = new();

        private readonly IMeasurementTypeService _measurementTypeService;

        public MeasurementGroupSchemaMeasurementTypeSettingsFormViewModel() : base()
        {
            _measurementTypeService = GetRequiredService<IMeasurementTypeService>();

            var measurementTypes = _measurementTypeService.GetAll();
            CreateMeasurementTypes(measurementTypes);
        }

        private void CreateMeasurementTypes(IEnumerable<MeasurementType> measurementTypes)
        {
            MeasurementTypes.Clear();
            foreach (var colorScheme in measurementTypes)
            {
                MeasurementTypes.Add(colorScheme);
            }
        }

        public override void PrepareModel()
        {
            base.PrepareModel();
            Model.MeasurementTypeId = MeasurementTypes[SelectedMeasurementTypeIndex].Id;
        }

        protected override void OnModelSet()
        {
            base.OnModelSet();
            var index = MeasurementTypes.ToList().FindIndex(x => x?.Id.Equals(Model?.MeasurementTypeId) ?? false);
            SelectedMeasurementTypeIndex = (index != -1) ? index : 0;
        }
    }
}