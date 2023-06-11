using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public class MeasurementStore : StoreBase<Measurement>, IMeasurementStore
    {
        public MeasurementStore(IMeasurementService service) : base(service)
        {
        }
    }
}
