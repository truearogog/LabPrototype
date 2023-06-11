using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public abstract class ServiceBase<TEntity, TModel> : IServiceBase<TModel>
        where TEntity : EntityBase
        where TModel : PresentationModelBase
    {
        private readonly IRepositoryBase<TEntity> _repository;

        protected IMapper Mapper;
        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;

        public ServiceBase(IMapper mapper, IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
            Mapper = mapper;
        }

        public TModel Create(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            _repository.Create(entity);
            return Mapper.Map<TModel>(entity);
        }

        public async Task<TModel> CreateAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = await _repository.CreateAsync(entity);
            return Mapper.Map<TModel>(entity);
        }

        public IEnumerable<TModel> CreateRange(IEnumerable<TModel> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            _repository.CreateRange(entities);
            return Mapper.Map<IEnumerable<TModel>>(entities);
        }

        public async Task<IEnumerable<TModel>> CreateRangeAsync(IEnumerable<TModel> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            await _repository.CreateRangeAsync(entities);
            return Mapper.Map<IEnumerable<TModel>>(entities);
        }

        public void Delete(TModel model)
        {
            _repository.Delete(model.Id);
        }

        public void Delete(int modelId)
        {
            _repository.Delete(modelId);
        }

        public void DeleteRange(IEnumerable<TModel> models)
        {
            var modelIds = models.Select(x => x.Id) ?? Enumerable.Empty<int>();
            _repository.DeleteRange(modelIds);
        }

        public void DeleteRange(IEnumerable<int> modelIds)
        {
            _repository.DeleteRange(modelIds);
        }

        public IQueryable<TModel> GetAll()
        {
            return _repository.GetAll().ProjectTo<TModel>(MapperConfig);
        }

        public TModel? GetById(int id)
        {
            var entity = _repository.GetById(id);
            return Mapper.Map<TModel>(entity);
        }

        public async Task<TModel?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return Mapper.Map<TModel>(entity);
        }

        public TModel Update(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = _repository.Update(entity);
            return Mapper.Map<TModel>(entity);
        }

        public IEnumerable<TModel> UpdateRange(IEnumerable<TModel> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            _repository.UpdateRange(entities);
            return Mapper.Map<IEnumerable<TModel>>(entities);
        }
    }
}
