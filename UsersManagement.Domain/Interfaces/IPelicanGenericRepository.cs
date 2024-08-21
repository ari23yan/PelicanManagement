using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Interfaces
{
    public interface IPelicanGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<bool> IsExist(Expression<Func<T, bool>> filter);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task Remove(T entity);
        Task RemoveRangeAsync(List<T> entity);
        Task UpdateAsync(T item);
    }
}
