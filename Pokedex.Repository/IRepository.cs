using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pokedex.Repository
{
    public interface IRepository<T> 
    {
        Task<List<T>> GetAsync();

        Task<T> FindByIdAsync(int id);

        void Add(T entity);

        void Remove(T entity);

        void Remove(int id);

        IReadOnlyList<T> Select(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          List<string> includes = null,
          int? page = null,
          int? pageSize = null);

        Task<IReadOnlyList<T>> SelectAsync(Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          List<string> includes = null,
          int? page = null,
          int? pageSize = null);
    }
}
