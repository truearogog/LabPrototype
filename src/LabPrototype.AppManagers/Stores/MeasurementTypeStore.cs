using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public class MeasurementTypeStore : StoreBase<MeasurementType>, IMeasurementTypeStore
    {
        public MeasurementTypeStore(IMeasurementTypeService service) : base(service)
        {
        }
    }
}
