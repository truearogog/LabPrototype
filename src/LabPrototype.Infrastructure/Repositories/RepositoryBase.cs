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
            foreach (var entity in entities)
            {
                DbSet.Add(entity);
            }
            DbContext.SaveChanges();
            return entities;
        }
        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.Created = DateTime.Now;
                entity.Updated = DateTime.Now;
                await DbSet.AddAsync(entity);
            }
            await DbContext.SaveChangesAsync();
            return entities;
        }
        public T? Update(T entity)
        {
            var entry = DbSet.Find(entity.Id);
            if (entry == null)
            {
                return default;
            }
            entity.Updated = DateTime.Now;
            DbContext.Entry(entry).CurrentValues.SetValues(entity);
            DbContext.SaveChanges();
            return entity;
        }
        public async Task<T?> UpdateAsync(T entity)
        {
            var entry = await DbSet.FindAsync(entity.Id);
            if (entry == null)
            {
                return default;
            }
            entity.Updated = DateTime.Now;
            DbContext.Entry(entry).CurrentValues.SetValues(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            var updatedEntities = new List<T>();
            foreach (var entity in entities)
            {
                var entry = DbSet.Find(entity.Id);
                if (entry == null)
                {
                    continue;
                }
                entity.Updated = DateTime.Now;
                DbContext.Entry(entry).CurrentValues.SetValues(entity);

                updatedEntities.Add(entity);
            }
            DbContext.SaveChanges();
            return updatedEntities;
        }
        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            var updatedEntities = new List<T>();
            foreach (var entity in entities)
            {
                var entry = await DbSet.FindAsync(entity.Id);
                if (entry == null)
                {
                    continue;
                }
                entity.Updated = DateTime.Now;
                DbContext.Entry(entry).CurrentValues.SetValues(entity);

                updatedEntities.Add(entity);
            }
            await DbContext.SaveChangesAsync();
            return updatedEntities;
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
