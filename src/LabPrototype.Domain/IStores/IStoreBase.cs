using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IStores
{
    public interface IStoreBase<T> where T : PresentationModelBase
    {
        event Action<IEnumerable<T>> ModelsLoaded;
        event Action<T> ModelCreated;
        event Action<T> ModelUpdated;
        event Action<int> ModelDeleted;

        void Load();
        void Create(T model);
        void Update(T model);
        void Delete(int modelId);
    }
}
