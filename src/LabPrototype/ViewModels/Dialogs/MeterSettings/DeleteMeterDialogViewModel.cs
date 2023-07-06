using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.ViewModels.Dialogs.MeterSettings
{
    public class DeleteMeterDialogViewModel : DeleteDialogViewModelBase<
        Meter, 
        IMeterService, 
        IMeterStore>
    {
        public DeleteMeterDialogViewModel() : base() { }
    }
}
