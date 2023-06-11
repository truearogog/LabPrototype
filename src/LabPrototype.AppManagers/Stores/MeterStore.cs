using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public class MeterStore : StoreBase<Meter>, IMeterStore
    {
        public MeterStore(IMeterService service) : base(service)
        {
        }
    }
}
