using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IServices
{
    public interface IServiceBase<TModel> 
        where TModel : PresentationModelBase
    {
        TModel Create(TModel model);
        Task<TModel> CreateAsync(TModel model);
        IEnumerable<TModel> CreateRange(IEnumerable<TModel> models);
        Task<IEnumerable<TModel>> CreateRangeAsync(IEnumerable<TModel> models);
        TModel Update(TModel model);
        IEnumerable<TModel> UpdateRange(IEnumerable<TModel> models);
        void Delete(TModel model);
        void Delete(int modelId);
        void DeleteRange(IEnumerable<TModel> models);
        void DeleteRange(IEnumerable<int> modelIds);
        TModel? GetById(int id);
        Task<TModel?> GetByIdAsync(int id);
        IQueryable<TModel> GetAll();
    }
}
