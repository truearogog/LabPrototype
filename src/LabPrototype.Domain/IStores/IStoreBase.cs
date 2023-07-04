using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IStores
{
    public interface IStoreBase<T> where T : PresentationModelBase
    {
        event Action<T> ModelCreated;
        event Action<T?> ModelUpdated;
        event Action<int> ModelDeleted;

        T? Create(IServiceBase<T> service, T model);
        T? Update(IServiceBase<T> service, T model);
        void Delete(IServiceBase<T> service, int modelId);
    }
}
