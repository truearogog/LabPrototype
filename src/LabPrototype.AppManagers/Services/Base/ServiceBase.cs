using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;
using System.Linq.Expressions;

namespace LabPrototype.AppManagers.Services
{
    public abstract class ServiceBase<TEntity, T, TRepository> : IServiceBase<T>
        where TEntity : EntityBase
        where T : PresentationModelBase
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

        public T Create(T model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Repository.Create(entity);
            return Mapper.Map<T>(entity);
        }

        public async Task<T> CreateAsync(T model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = await Repository.CreateAsync(entity);
            return Mapper.Map<T>(entity);
        }

        public IEnumerable<T> CreateRange(IEnumerable<T> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            Repository.CreateRange(entities);
            return Mapper.Map<IEnumerable<T>>(entities);
        }

        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            await Repository.CreateRangeAsync(entities);
            return Mapper.Map<IEnumerable<T>>(entities);
        }

        public void Delete(T model)
        {
            Repository.Delete(model.Id);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public void DeleteRange(IEnumerable<T> models)
        {
            var modelIds = models.Select(x => x.Id) ?? Enumerable.Empty<int>();
            Repository.DeleteRange(modelIds);
        }

        public void DeleteRange(IEnumerable<int> modelIds)
        {
            Repository.DeleteRange(modelIds);
        }

        public T? GetById(int id)
        {
            var entity = Repository.GetById(id);
            return Mapper.Map<T>(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return Mapper.Map<T>(entity);
        }

        public T? Update(T model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = Repository.Update(entity);
            return Mapper.Map<T>(entity);
        }

        public async Task<T?> UpdateAsync(T model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = await Repository.UpdateAsync(entity);
            return Mapper.Map<T?>(entity);
        }


        public IEnumerable<T> UpdateRange(IEnumerable<T> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            entities = Repository.UpdateRange(entities);
            return Mapper.Map<IEnumerable<T>>(entities);
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> models)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            entities = await Repository.UpdateRangeAsync(entities);
            return Mapper.Map<IEnumerable<T>>(entities);
        }

        public IQueryable<T> GetAll()
        {
            return
                Repository
                .GetAll()
                .ProjectTo<T>(MapperConfig);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return
                Repository
                .GetAll()
                .ProjectTo<T>(MapperConfig)
                .Where(predicate);
        }
    }
}
