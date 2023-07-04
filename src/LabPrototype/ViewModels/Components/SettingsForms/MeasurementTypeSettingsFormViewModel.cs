using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components.SettingsForms
{
    public class MeasurementTypeSettingsFormViewModel : SettingsFormViewModelBase<MeasurementType, IMeasurementTypeStore>
    {
        private int _selectedColorSchemeIndex;
        public int SelectedColorSchemeIndex
        {
            get => _selectedColorSchemeIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedColorSchemeIndex, value);
        }

        public ObservableCollection<ColorScheme> ColorSchemes { get; set; } = new();

        private readonly IColorSchemeService _colorSchemeService;

        public MeasurementTypeSettingsFormViewModel() : base()
        {
            _colorSchemeService = GetRequiredService<IColorSchemeService>();

            var colorSchemes = _colorSchemeService.GetAll();
            CreateColorSchemes(colorSchemes);
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

        protected override void OnModelSet()
        {
            SelectedColorSchemeIndex = ColorSchemes.ToList().FindIndex(x => x?.Id.Equals(Model?.ColorSchemeId) ?? false);
        }
    }
}
