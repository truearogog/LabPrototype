using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase, new()
    {
        private readonly LabDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(LabDbContext dbContext, DbSet<T> dbSet)
        {
            _dbContext = dbContext;
            _dbSet = dbSet;
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public IEnumerable<T> CreateRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }
        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }
        public void DeleteRange(IEnumerable<int> entityIds)
        {
            _dbSet.RemoveRange(entityIds.Select(x => new T { Id = x }));
            _dbContext.SaveChanges();
        }
        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
    }
}
