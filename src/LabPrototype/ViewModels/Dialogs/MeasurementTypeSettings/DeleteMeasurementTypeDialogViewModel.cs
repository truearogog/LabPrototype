using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.ViewModels.Dialogs.MeasurementTypeSettings
{
    public class DeleteMeasurementTypeDialogViewModel : DeleteDialogViewModelBase<
        MeasurementType, 
        IMeasurementTypeService, 
        IMeasurementTypeStore>
    {
        public DeleteMeasurementTypeDialogViewModel() : base() { }
    }
}
