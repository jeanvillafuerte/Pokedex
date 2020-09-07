using Microsoft.EntityFrameworkCore;
using Pokedex.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pokedex.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IObjectState, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly IDataContextAsync _context;

        public Repository(IDataContextAsync context)
        {
            _context = context;
            var dbContext = context as DbContext;

            if (dbContext != null)
            {
                _dbSet = dbContext.Set<T>();
            }
        }

        public void Add(T entity)
        {
            entity.ObjectState = ObjectState.Added;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Remove(int id)
        {
            var entity = _dbSet.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public IReadOnlyList<T> Select(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<T>> SelectAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, int? page = null, int? pageSize = null)
        {
            return await Task.Factory.StartNew(() => { return Select(filter, orderBy, includes, page, pageSize); });
        }
    }
}
