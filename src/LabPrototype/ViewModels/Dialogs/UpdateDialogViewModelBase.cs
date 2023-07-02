using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;
using LabPrototype.ViewModels.Models;
using System;

namespace LabPrototype.ViewModels.Dialogs
{
    public class UpdateDialogViewModelBase<T, TService, TStore, TSettingsForm> : ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>
        where T : PresentationModelBase, new()
        where TService : IServiceBase<T>
        where TStore : IStoreBase<T>
        where TSettingsForm : SettingsFormViewModelBase<T, TStore>, new()
    {
        public TSettingsForm SettingsFormViewModel { get; }

        public UpdateDialogViewModelBase()
        {
            var service = GetRequiredService<TService>();
            SettingsFormViewModel = new TSettingsForm();
            SettingsFormViewModel.Activate(CloseCommand, (store, model) =>
            {
                store.Update(service, model);
                Close();
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
