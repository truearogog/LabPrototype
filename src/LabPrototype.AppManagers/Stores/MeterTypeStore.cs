using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public class MeterTypeStore : StoreBase<MeterType>, IMeterTypeStore
    {
        public MeterTypeStore(IMeterTypeService service) : base(service)
        {
        }
    }
}
