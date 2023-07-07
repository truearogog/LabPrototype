﻿using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Forms;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.SettingsForms;

namespace LabPrototype.ViewModels.Dialogs.ColorSchemeSettings
{
    public class UpdateColorSchemeDialogViewModel : UpdateDialogViewModelBase<
        ColorScheme, 
        IColorSchemeService, 
        IColorSchemeStore, 
        ColorSchemeForm,
        ColorSchemeSettingsFormViewModel>
    {
        public UpdateColorSchemeDialogViewModel() : base() { }
    }
}
