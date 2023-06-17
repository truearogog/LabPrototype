using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public abstract class ServiceBase<TEntity, TModel, TRepository> : IServiceBase<TModel>
        where TEntity : EntityBase
        where TModel : PresentationModelBase
        where TRepository : IRepositoryBase<TEntity>
    {
        protected readonly TRepository Repository;

        protected IMapper Mapper;
        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;

        public ServiceBase(IMapper mapper, TRepository repository)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public TModel Create(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Repository.Create(entity);
            return Mapper.Map<TModel>(entity);
        }

        public async Task<TModel> CreateAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = await Repository.CreateAsync(entity);
            return Mapper.Map<TModel>(entity);
        }

        public IEnumerable<TModel> CreateRange(IEnumerable<TModel> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            Repository.CreateRange(entities);
            return Mapper.Map<IEnumerable<TModel>>(entities);
        }

        public async Task<IEnumerable<TModel>> CreateRangeAsync(IEnumerable<TModel> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            await Repository.CreateRangeAsync(entities);
            return Mapper.Map<IEnumerable<TModel>>(entities);
        }

        public void Delete(TModel model)
        {
            Repository.Delete(model.Id);
        }

        public void Delete(int modelId)
        {
            Repository.Delete(modelId);
        }

        public void DeleteRange(IEnumerable<TModel> models)
        {
            var modelIds = models.Select(x => x.Id) ?? Enumerable.Empty<int>();
            Repository.DeleteRange(modelIds);
        }

        public void DeleteRange(IEnumerable<int> modelIds)
        {
            Repository.DeleteRange(modelIds);
        }

        public TModel? GetById(int id)
        {
            var entity = Repository.GetById(id);
            return Mapper.Map<TModel>(entity);
        }

        public async Task<TModel?> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return Mapper.Map<TModel>(entity);
        }

        public TModel Update(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Repository.Update(entity);
            return Mapper.Map<TModel>(entity);
        }

        public IEnumerable<TModel> UpdateRange(IEnumerable<TModel> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            Repository.UpdateRange(entities);
            return Mapper.Map<IEnumerable<TModel>>(entities);
        }

        public IQueryable<TModel> GetAll()
        {
            return Repository.GetAll().ProjectTo<TModel>(MapperConfig);
        }

        public IEnumerable<TModel> GetAll(Func<TModel, bool> predicate)
        {
            return 
                Repository
                .GetAll()
                .ProjectTo<TModel>(MapperConfig)
                .Where(predicate)
                .ToList();
        }
    }
}
