using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Framework.Models;
using LabPrototype.ViewModels.Components.ModelSettings;
using LabPrototype.ViewModels.Models;
using System;

namespace LabPrototype.ViewModels.Dialogs
{
    public class UpdateDialogViewModelBase<T, TService, TStore, TForm, TSettingsForm> : ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>
        where T : PresentationModelBase, new()
        where TService : IServiceBase<T>
        where TStore : IStoreBase<T>
        where TForm : FormBase, new()
        where TSettingsForm : SettingsFormViewModelBase<T, TStore, TForm>, new()
    {
        public TSettingsForm SettingsFormViewModel { get; }

        public UpdateDialogViewModelBase()
        {
            var service = GetRequiredService<TService>();
            SettingsFormViewModel = new TSettingsForm();
            SettingsFormViewModel.Activate(CloseCommand, (store, model) =>
            {
                var updatedModel = store.Update(service, model);
                Close();
                return updatedModel;
            });
        }

        public override void Dispose()
        {
            SettingsFormViewModel.Dispose();

            base.Dispose();
        }

        public override void Activate(ModelNavigationParameter<T> parameter)
        {
            SettingsFormViewModel.Model = parameter.Model ?? throw new Exception();
        }
    }
}
