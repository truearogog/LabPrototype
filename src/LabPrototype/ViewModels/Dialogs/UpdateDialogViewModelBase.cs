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
        where TSettingsForm : SettingsFormViewModelBase<T, TStore>
    {
        public TSettingsForm SettingsFormViewModel { get; }

        public UpdateDialogViewModelBase(Func<ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>, TService, TSettingsForm> settingsFormFactory)
        {
            var service = GetRequiredService<TService>();
            SettingsFormViewModel = settingsFormFactory(this, service);
        }

        public override void Activate(ModelNavigationParameter<T> parameter)
        {
            SettingsFormViewModel.Model = parameter.Model ?? throw new Exception();
        }
    }
}
