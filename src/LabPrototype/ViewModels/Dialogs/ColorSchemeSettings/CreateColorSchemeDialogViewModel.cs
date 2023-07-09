using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.ColorSchemeSettings
{
    public class CreateColorSchemeDialogViewModel : CreateDialogViewModelBase<
        ColorScheme, 
        IColorSchemeService, 
        IColorSchemeStore, 
        ColorSchemeForm,
        ColorSchemeSettingsFormViewModel>
    {
        public CreateColorSchemeDialogViewModel() : base() { }
    }
}
