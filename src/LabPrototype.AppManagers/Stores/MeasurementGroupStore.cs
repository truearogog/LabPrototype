using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public class MeasurementGroupStore : StoreBase<MeasurementGroup>, IMeasurementGroupStore
    {
        public MeasurementGroupStore(IMeasurementGroupService service) : base(service)
        {
        }
    }
}
