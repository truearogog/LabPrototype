using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.ViewModels.Dialogs.MeterTypeSettings
{
    public class DeleteMeterTypeDialogViewModel : DeleteDialogViewModelBase<
        MeterType, 
        IMeterTypeService, 
        IMeterTypeStore>
    {
        public DeleteMeterTypeDialogViewModel() : base() { }
    }
}
