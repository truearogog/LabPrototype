using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.ModelSettings;

namespace LabPrototype.ViewModels.Components.SettingsForms
{
    public class ColorSchemeSettingsFormViewModel : SettingsFormViewModelBase<ColorScheme, IColorSchemeStore, ColorSchemeForm>
    {
        public ColorSchemeSettingsFormViewModel() : base() { }
    }
}
