using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<bool> IsExist(Expression<Func<T, bool>> filter);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync();
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T item);

        Task SaveAsync();
    }
}
