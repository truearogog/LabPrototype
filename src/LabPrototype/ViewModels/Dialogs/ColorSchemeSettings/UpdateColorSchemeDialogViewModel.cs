using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.ColorSchemeSettings
{
    public class UpdateColorSchemeDialogViewModel : UpdateDialogViewModelBase<ColorScheme, IColorSchemeService, IColorSchemeStore, ColorSchemeSettingsFormViewModel>
    {
        public UpdateColorSchemeDialogViewModel() : base() { }
    }
}
