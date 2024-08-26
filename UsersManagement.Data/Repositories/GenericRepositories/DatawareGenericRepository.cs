using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Data.Context;
using UsersManagement.Domain.Interfaces.GenericRepositories;

namespace UsersManagement.Data.Repositories.GenericRepositories
{
    public class DatawareGenericRepository<T> : IDatawareGenericRepository<T> where T : class
    {
        protected readonly DatawareDbContext Context;
        protected DbSet<T> entities;

        public DatawareGenericRepository(DatawareDbContext context)
        {
            Context = context;
            entities = Context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await entities.AddAsync(entity);
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await entities.Where(filter).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await entities.SingleOrDefaultAsync(filter);
        }

        public async Task Remove(T entity)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                entities.Remove(entity);
                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                entities.Update(entity);
                await Context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter)
        {
            return await entities.Where(filter).CountAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> filter)
        {
            return await entities.AnyAsync(filter);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                await entities.AddRangeAsync(entity);
                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task RemoveRangeAsync(List<T> entity)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                entities.RemoveRange(entity);
                await Context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await entities.FirstOrDefaultAsync(filter);
        }
        public async Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await entities.LastOrDefaultAsync(filter);
        }
    }
}
