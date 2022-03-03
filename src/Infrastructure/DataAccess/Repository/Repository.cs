using System.Linq.Expressions;
using Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Repository : IRepository
    {
        private readonly BaseContext m_dbContext;

        public Repository(BaseContext context)
        {
            m_dbContext = context;
        }

        public Task<T> GetItem<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return m_dbContext.Set<T>().FirstOrDefaultAsync(filter);
        }

        public IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            return filter == null ? m_dbContext.Set<T>().AsNoTracking() : m_dbContext.Set<T>().AsNoTracking().Where(filter);
        }

        public T Add<T>(T model) where T : class
        {
            var entity = m_dbContext.Set<T>().Add(model).Entity;
            return entity;
        }

        public void Remove<T>(Expression<Func<T, bool>> filter) where T : class
        {
            var entitySet = m_dbContext.Set<T>();
            entitySet.RemoveRange(entitySet.Where(filter));
        }

        public void Update<T>(T model) where T : class
        {
            m_dbContext.Set<T>().Update(model);
        }

        public Task SaveChanges()
        {
            return m_dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync<T>() where T : class
        {
            return await m_dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await m_dbContext.Set<T>().AnyAsync(predicate);
        }
    }
}