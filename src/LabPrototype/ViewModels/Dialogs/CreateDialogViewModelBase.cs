using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;
using LabPrototype.ViewModels.Components.ModelSettings;

namespace LabPrototype.ViewModels.Dialogs
{
    public class CreateDialogViewModelBase<T, TService, TStore, TForm, TSettingsForm> : DialogViewModelBase
        where T : PresentationModelBase, new()
        where TService : IServiceBase<T>
        where TStore : IStoreBase<T>
        where TForm : FormBase, new()
        where TSettingsForm : SettingsFormViewModelBase<T, TStore, TForm>, new()
    {
        public TSettingsForm SettingsFormViewModel { get; } 

        public CreateDialogViewModelBase()
        {
            var service = GetRequiredService<TService>();
            SettingsFormViewModel = new TSettingsForm();
            SettingsFormViewModel.Activate(CloseCommand, (store, model) =>
            {
                var createdModel = store.Create(service, model);
                Close();
                return createdModel;
            });
        }

        public override void Dispose()
        {
            SettingsFormViewModel.Dispose();

            base.Dispose();
        }
    }
}
