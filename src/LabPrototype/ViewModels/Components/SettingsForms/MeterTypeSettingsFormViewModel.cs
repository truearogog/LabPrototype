using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.SettingsForms
{
    public class MeterTypeSettingsFormViewModel : SettingsFormViewModelBase<MeterType, IMeterTypeStore>
    {
        private int _selectedColorSchemeIndex;
        public int SelectedColorSchemeIndex
        {
            get => _selectedColorSchemeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedColorSchemeIndex, value);
        }

        public ObservableCollection<ColorScheme> ColorSchemes { get; set; } = new();
        public ObservableCollection<MeterTypeMeasurementTypeSettingsFormViewModel> MeterTypeMeasurementTypeForms { get; set; } = new();

        private readonly IColorSchemeService _colorSchemeService;
        private readonly IMeterTypeMeasurementTypeService _meterTypeMeasurementTypeService;
        private readonly IMeterTypeMeasurementTypeStore _meterTypeMeasurementTypeStore;

        public ICommand AddMeterTypeMeasurementTypeCommand { get; }

        public MeterTypeSettingsFormViewModel() : base()
        {
            _colorSchemeService = GetRequiredService<IColorSchemeService>();
            _meterTypeMeasurementTypeService = GetRequiredService<IMeterTypeMeasurementTypeService>();
            _meterTypeMeasurementTypeStore = GetRequiredService<IMeterTypeMeasurementTypeStore>();

            var colorSchemes = _colorSchemeService.GetAll();
            CreateColorSchemes(colorSchemes);

            AddMeterTypeMeasurementTypeCommand = ReactiveCommand.Create(() => AddMeterTypeMeasurementTypeForm());
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
            Model.ColorSchemeId = ColorSchemes[SelectedColorSchemeIndex].Id;
        }

        protected override void AfterSubmit(MeterType? model)
        {
            if (model != default)
            {
                var meterTypeMeasurementTypes = _meterTypeMeasurementTypeService.GetAll(x => x.MeterTypeId.Equals(model.Id));

                MeterTypeMeasurementTypeForms.ToList().ForEach(x => {
                    x.PrepareModel();
                    x.Model.MeterTypeId = model.Id;
                });

                var meterTypeMeasurementTypesToCreate = MeterTypeMeasurementTypeForms
                    .Select(x => x.Model)
                    .Where(x => x.Id == default);
                foreach (var meterTypeMeasurementTypeToCreate in meterTypeMeasurementTypesToCreate)
                {
                    _meterTypeMeasurementTypeStore.Create(_meterTypeMeasurementTypeService, meterTypeMeasurementTypeToCreate);
                }

                var meterTypeMeasurementTypesToUpdate = MeterTypeMeasurementTypeForms
                    .Select(x => x.Model)
                    .Where(x => meterTypeMeasurementTypes.Any(y => y.Id.Equals(x.Id)));
                foreach (var meterTypeMeasurementTypeToUpdate in meterTypeMeasurementTypesToUpdate)
                {
                    _meterTypeMeasurementTypeStore.Update(_meterTypeMeasurementTypeService, meterTypeMeasurementTypeToUpdate);
                }

                var meterTypeMeasurementTypeIdsToDelete = meterTypeMeasurementTypes
                    .Select(x => x.Id)
                    .Where(x => MeterTypeMeasurementTypeForms.Any(y => y.Model.Id.Equals(x)) == false);
                foreach (var meterTypeMeasurementTypeIdToDelete in meterTypeMeasurementTypeIdsToDelete)
                {
                    _meterTypeMeasurementTypeStore.Delete(_meterTypeMeasurementTypeService, meterTypeMeasurementTypeIdToDelete);
                }
            }
        }

        protected override void OnModelSet()
        {
            SelectedColorSchemeIndex = ColorSchemes.ToList().FindIndex(x => x?.Id.Equals(Model?.ColorSchemeId) ?? false);

            var meterTypeMeasurementTypes = _meterTypeMeasurementTypeService.GetAll(x => x.MeterTypeId.Equals(Model.Id));
            CreateMeterTypeMeasurementTypeForms(meterTypeMeasurementTypes);
        }

        private void CreateMeterTypeMeasurementTypeForms(IEnumerable<MeterTypeMeasurementType> meterTypeMeasurementTypes)
        {
            MeterTypeMeasurementTypeForms.Clear();
            foreach (var meterTypeMeasurementType in meterTypeMeasurementTypes)
            {
                AddMeterTypeMeasurementTypeForm(meterTypeMeasurementType);
            }
        }

        private void AddMeterTypeMeasurementTypeForm(MeterTypeMeasurementType? meterTypeMeasurementType = null)
        {
            var viewModel = new MeterTypeMeasurementTypeSettingsFormViewModel();
            viewModel.Model = meterTypeMeasurementType ?? new MeterTypeMeasurementType() { MeterTypeId = Model.Id };
            var deleteCommand = ReactiveCommand.Create(() => MeterTypeMeasurementTypeForms.Remove(viewModel));
            viewModel.Activate(deleteCommand, null);
            MeterTypeMeasurementTypeForms.Add(viewModel);
        }
    }
}
