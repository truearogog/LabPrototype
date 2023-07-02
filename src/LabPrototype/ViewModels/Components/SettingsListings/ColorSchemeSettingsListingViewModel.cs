using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.Settings;
using LabPrototype.ViewModels.Components.SettingsListingItems;
using LabPrototype.ViewModels.Dialogs.ColorSchemeSettings;
using LabPrototype.Views.Dialogs.ColorSchemeSettings;

namespace LabPrototype.ViewModels.Components.SettingsListings
{
    public class ColorSchemeSettingsListingViewModel : SettingsListingViewModelBase<
        ColorScheme,
        IColorSchemeService,
        IColorSchemeStore,
        CreateColorSchemeDialog, CreateColorSchemeDialogViewModel,
        UpdateColorSchemeDialog, UpdateColorSchemeDialogViewModel,
        DeleteColorSchemeDialog, DeleteColorSchemeDialogViewModel,
        ColorSchemeSettingsListingItemViewModel>
    {
        public ColorSchemeSettingsListingViewModel(WindowViewModelBase parentWindow) : base(parentWindow)
        {
        }
    }
}
