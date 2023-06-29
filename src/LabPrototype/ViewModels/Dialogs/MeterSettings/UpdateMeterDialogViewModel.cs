using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.ModelSettings;
using LabPrototype.ViewModels.Models;

namespace LabPrototype.ViewModels.Dialogs.MeterSettings
{
    public class UpdateMeterDialogViewModel : UpdateDialogViewModelBase<Meter, IMeterService, IMeterStore, MeterSettingsFormViewModel>
    {
        public UpdateMeterDialogViewModel() : base((dialog, service) =>
        {
            return new MeterSettingsFormViewModel(dialog.CloseCommand, (store, model) =>
            {
                store.Update(service, model);
                dialog.Close();
            });
        })
        {

        }

        public override void Activate(ModelNavigationParameter<Meter> parameter)
        {
            base.Activate(parameter);
        }
    }
}
