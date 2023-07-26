using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.ModelSettings;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.SettingsForms
{
    public class MeterTypeSettingsFormViewModel : SettingsFormViewModelBase<MeterType, IMeterTypeStore, MeterTypeForm>
    {
        private int _selectedColorSchemeIndex;
        public int SelectedColorSchemeIndex
        {
            get => _selectedColorSchemeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedColorSchemeIndex, value);
        }

        public ObservableCollection<ColorScheme> ColorSchemes { get; set; } = new();
        public ObservableCollection<MeasurementGroupSchemaMeasurementTypeSettingsFormViewModel> MeasurementGroupSchemaMeasurementTypeForms { get; set; } = new();

        private readonly IColorSchemeService _colorSchemeService;
        private readonly IMeasurementGroupSchemaService _measurementGroupSchemeService;
        private readonly IMeasurementGroupSchemaStore _measurementGroupSchemeStore;
        private readonly IMeasurementGroupSchemaMeasurementTypeService _measurementGroupSchemaMeasurementTypeService;
        private readonly IMeasurementGroupSchemaMeasurementTypeStore _measurementGroupSchemaMeasurementTypeStore;

        public ICommand AddMeasurementGroupSchemeMeasurementTypeCommand { get; }

        public MeterTypeSettingsFormViewModel() : base()
        {
            _colorSchemeService = GetRequiredService<IColorSchemeService>();
            _measurementGroupSchemeService = GetRequiredService<IMeasurementGroupSchemaService>();
            _measurementGroupSchemeStore = GetRequiredService<IMeasurementGroupSchemaStore>();
            _measurementGroupSchemaMeasurementTypeService = GetRequiredService<IMeasurementGroupSchemaMeasurementTypeService>();
            _measurementGroupSchemaMeasurementTypeStore = GetRequiredService<IMeasurementGroupSchemaMeasurementTypeStore>();

            var colorSchemes = _colorSchemeService.GetAll();
            CreateColorSchemes(colorSchemes);

            AddMeasurementGroupSchemeMeasurementTypeCommand = ReactiveCommand.Create(() => AddMeasurementGroupSchemeMeasurementTypeForm());
        }

        private void CreateColorSchemes(IEnumerable<ColorScheme> colorSchemes)
        {
            ColorSchemes.Clear();
            foreach (var colorScheme in colorSchemes)
            {
                ColorSchemes.Add(colorScheme);
            }
        }

        public override void PrepareModel()
        {
            base.PrepareModel();
            Model.ColorSchemeId = ColorSchemes[SelectedColorSchemeIndex].Id;
        }

        protected override void AfterSubmit(MeterType? model)
        {
            base.AfterSubmit(model);
            if (model is not null)
            {
                // create new measurement group scheme
                var measurementGroupSchema = new MeasurementGroupSchema { MeterTypeId = model.Id };
                measurementGroupSchema = _measurementGroupSchemeStore.Create(_measurementGroupSchemeService, measurementGroupSchema) ?? throw new Exception();

                MeasurementGroupSchemaMeasurementTypeForms.ToList().ForEach(x =>
                {
                    x.PrepareModel();
                    x.Model.MeasurementGroupSchemaId = measurementGroupSchema.Id;
                });
                var measurementGroupSchemaMeasurementTypes = MeasurementGroupSchemaMeasurementTypeForms.Select(x => x.Model);
                measurementGroupSchemaMeasurementTypes.ToList().ForEach(x => x.Id = 0);

                foreach (var measurementGroupSchemaMeasurementType in measurementGroupSchemaMeasurementTypes)
                {
                    _measurementGroupSchemaMeasurementTypeStore.Create(_measurementGroupSchemaMeasurementTypeService, measurementGroupSchemaMeasurementType);
                }
            }
        }

        protected override void OnModelSet()
        {
            base.OnModelSet();
            SelectedColorSchemeIndex = ColorSchemes.ToList().FindIndex(x => x?.Id.Equals(Model?.ColorSchemeId) ?? false);

            var latestScheme = _measurementGroupSchemeService
                .GetAll(x => x.MeterTypeId.Equals(Model.Id))
                .OrderByDescending(x => x.Created)
                .FirstOrDefault();
            var measurementGroupSchemeMeasurementTypes = _measurementGroupSchemaMeasurementTypeService
                .GetAll(x => x.MeasurementGroupSchemaId.Equals(latestScheme?.Id ?? 0));
            CreateMeasurementGroupSchemeMeasurementTypeForms(measurementGroupSchemeMeasurementTypes);
        }

        private void CreateMeasurementGroupSchemeMeasurementTypeForms(IEnumerable<MeasurementGroupSchemaMeasurementType> measurementGroupSchemeMeasurementTypes)
        {
            MeasurementGroupSchemaMeasurementTypeForms.Clear();
            foreach (var measurementGroupSchemeMeasurementType in measurementGroupSchemeMeasurementTypes)
            {
                AddMeasurementGroupSchemeMeasurementTypeForm(measurementGroupSchemeMeasurementType);
            }
        }

        private void AddMeasurementGroupSchemeMeasurementTypeForm(MeasurementGroupSchemaMeasurementType? measurementGroupSchemeMeasurementType = null)
        {
            var viewModel = new MeasurementGroupSchemaMeasurementTypeSettingsFormViewModel
            {
                Model = measurementGroupSchemeMeasurementType ?? new MeasurementGroupSchemaMeasurementType(),
            };
            var deleteCommand = ReactiveCommand.Create(() => MeasurementGroupSchemaMeasurementTypeForms.Remove(viewModel));
            viewModel.Activate(deleteCommand, null);
            MeasurementGroupSchemaMeasurementTypeForms.Add(viewModel);
        }
    }
}
