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

        public T? Create(IServiceBase<T> service, T model)
        {
            var createdModel = service.Create(model);
            ModelCreated?.Invoke(createdModel);
            return createdModel;
        }

        public T? Update(IServiceBase<T> service, T model)
        {
            var updatedModel = service.Update(model);
            if (updatedModel is not null)
            {
                ModelUpdated?.Invoke(updatedModel);
            }
            return updatedModel;
        }

        public void Delete(IServiceBase<T> service, int id)
        {
            service.Delete(id);
            ModelDeleted?.Invoke(id);
        }
    }
}
