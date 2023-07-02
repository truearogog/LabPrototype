using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateDialogViewModelBase<T, TService, TStore, TSettingsForm> : DialogViewModelBase
        where T : PresentationModelBase, new()
        where TService : IServiceBase<T>
        where TStore : IStoreBase<T>
        where TSettingsForm : SettingsFormViewModelBase<T, TStore>, new()
    {
        public TSettingsForm SettingsFormViewModel { get; } 

        public CreateDialogViewModelBase()
        {
            var service = GetRequiredService<TService>();
            SettingsFormViewModel = new TSettingsForm();
            SettingsFormViewModel.Activate(CloseCommand, (store, model) =>
            {
                store.Create(service, model);
                Close();
            });
        }

        public override void Dispose()
        {
            SettingsFormViewModel.Dispose();

            base.Dispose();
        }
    }
}
