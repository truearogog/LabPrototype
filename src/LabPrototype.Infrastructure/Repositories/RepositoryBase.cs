using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase, new()
    {
        protected readonly LabDbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(LabDbContext dbContext, DbSet<T> dbSet)
        {
            DbContext = dbContext;
            DbSet = dbSet;
        }

        public T Create(T entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
        public IEnumerable<T> CreateRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
            DbContext.SaveChanges();
            return entities;
        }
        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await DbContext.SaveChangesAsync();
            return entities;
        }
        public T Update(T entity)
        {
            DbSet.Update(entity);
            DbContext.SaveChanges();
            return entity;
        }
        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            DbSet.UpdateRange(entities);
            DbContext.SaveChanges();
            return entities;
        }
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }
        public void Delete(int entityId)
        {
            DbSet.Remove(new T { Id = entityId });
            DbContext.SaveChanges();
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
            DbContext.SaveChanges();
        }
        public void DeleteRange(IEnumerable<int> entityIds)
        {
            DbSet.RemoveRange(entityIds.Select(x => new T { Id = x }));
            DbContext.SaveChanges();
        }
        public T? GetById(int id)
        {
            return DbSet.Find(id);
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
        public IEnumerable<TOut> GetMany<TOut>(int id, Func<T, ICollection<TOut>?> getter)
        {
            var entity = GetById(id);
            if (entity is null)
                return Enumerable.Empty<TOut>();
            var many = getter(entity);
            if (many is null)
                return Enumerable.Empty<TOut>();
            return many;
        }
    }
}
