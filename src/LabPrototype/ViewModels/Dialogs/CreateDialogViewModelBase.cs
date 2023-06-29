using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;
using System;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateDialogViewModelBase<T, TService, TStore, TSettingsForm> : DialogViewModelBase
        where T : PresentationModelBase, new()
        where TService : IServiceBase<T>
        where TStore : IStoreBase<T>
        where TSettingsForm : SettingsFormViewModelBase<T, TStore>
    {
        public TSettingsForm SettingsFormViewModel { get; } 

        public CreateDialogViewModelBase(Func<DialogViewModelBase, TService, TSettingsForm> settingsFormFactory)
        {
            var service = GetRequiredService<TService>();
            SettingsFormViewModel = settingsFormFactory(this, service);
        }

        public override void Dispose()
        {
            SettingsFormViewModel.Dispose();

            base.Dispose();
        }
    }
}
