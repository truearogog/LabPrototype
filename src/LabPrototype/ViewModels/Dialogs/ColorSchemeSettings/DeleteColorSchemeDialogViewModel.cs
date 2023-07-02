using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.ViewModels.Dialogs.ColorSchemeSettings
{
    public class DeleteColorSchemeDialogViewModel : DeleteDialogViewModelBase<ColorScheme, IColorSchemeService, IColorSchemeStore>
    {
        public DeleteColorSchemeDialogViewModel() : base() { }
    }
}
