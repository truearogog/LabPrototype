using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public abstract class StoreBase<T> : IStoreBase<T>
        where T : PresentationModelBase
    {
        public event Action<T>? ModelCreated;
        public event Action<T>? ModelUpdated;
        public event Action<int>? ModelDeleted;

        public void Create(IServiceBase<T> service, T model)
        {
            model = service.Create(model);
            ModelCreated?.Invoke(model);
        }

        public void Update(IServiceBase<T> service, T model)
        {
            model = service.Update(model);
            ModelUpdated?.Invoke(model);
        }

        public void Delete(IServiceBase<T> service, int modelId)
        {
            service.Delete(modelId);
            ModelDeleted?.Invoke(modelId);
        }
    }
}
