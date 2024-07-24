﻿using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext Context;
        protected DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            Context = context;
            entities = Context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                await entities.AddAsync(entity);
                await SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
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

        public void Remove(T entity)
        {
            entities.Remove(entity);
        }

        public virtual async void Update(T entity)
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

        public async Task SaveAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter)
        {
            return await entities.Where(filter).CountAsync(filter);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> filter)
        {
            return await entities.AnyAsync(filter);
        }
    }
}
