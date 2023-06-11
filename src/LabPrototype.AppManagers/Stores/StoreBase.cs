using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public abstract class StoreBase<T> : IStoreBase<T>
        where T : PresentationModelBase
    {
        public event Action<IEnumerable<T>>? ModelsLoaded;
        public event Action<T>? ModelCreated;
        public event Action<T>? ModelUpdated;
        public event Action<int>? ModelDeleted;

        private readonly IServiceBase<T> _service;

        protected StoreBase(IServiceBase<T> service)
        {
            _service = service;
        }

        public void Load()
        {
            var models = _service.GetAll().ToList();
            ModelsLoaded?.Invoke(models);
        }

        public void Create(T model)
        {
            model = _service.Create(model);
            ModelCreated?.Invoke(model);
        }

        public void Update(T model)
        {
            model = _service.Update(model);
            ModelUpdated?.Invoke(model);
        }

        public void Delete(int modelId)
        {
            _service.Delete(modelId);
            ModelDeleted?.Invoke(modelId);
        }
    }
}
