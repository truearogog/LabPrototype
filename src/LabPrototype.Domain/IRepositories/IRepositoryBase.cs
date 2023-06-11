using LabPrototype.Domain.Entities;

namespace LabPrototype.Domain.IRepositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        IEnumerable<T> CreateRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(int entityId);
        void DeleteRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<int> entityIds);
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAll();
    }
}
