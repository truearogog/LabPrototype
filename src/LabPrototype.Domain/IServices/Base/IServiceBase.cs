using LabPrototype.Domain.Models.Presentation;
using System.Linq.Expressions;

namespace LabPrototype.Domain.IServices
{
    public interface IServiceBase<T> where T : PresentationModelBase
    {
        T Create(T model);
        Task<T> CreateAsync(T model);
        IEnumerable<T> CreateRange(IEnumerable<T> models);
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> models);
        T? Update(T model);
        Task<T?> UpdateAsync(T model);
        IEnumerable<T> UpdateRange(IEnumerable<T> models);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> models);
        void Delete(T model);
        void Delete(int id);
        void DeleteRange(IEnumerable<T> models);
        void DeleteRange(IEnumerable<int> modelIds);
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}
