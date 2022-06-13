using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<T, K>
    {
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> query = default, int skip = default, int take = default);
        Task<T> GetById(K id);
        void Add(T entity);
        void Update(T entity);
        Task Remove(K id);
        Task<int> SaveChangesAsync();
    }
}
