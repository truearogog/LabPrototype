using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.ColorSchemeSettings
{
    public class CreateColorSchemeDialogViewModel : CreateDialogViewModelBase<ColorScheme, IColorSchemeService, IColorSchemeStore, ColorSchemeSettingsFormViewModel>
    {
        public CreateColorSchemeDialogViewModel() : base() { }
    }
}
