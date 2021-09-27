using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<T> GetAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);
        Task AddAsync(T model);
        Task AddRangeAsync(IReadOnlyList<T> models);
        void Update(T model);
        void UpdateRangeAsync(IReadOnlyList<T> models);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}
