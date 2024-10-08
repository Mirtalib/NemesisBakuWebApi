﻿using Application.IRepositories.IRepository;
using Application.IRepositories.Repository;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Repository
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        DbSet<T> Table => _context.Set<T>();

        #region Add

        public async Task<bool> AddAsync(T entity)
        {
            var entry = await Table.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        #endregion


        #region Remove

        public bool Remove(T entity)
        {
            var entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var element = await Table.FindAsync(id);
            if (element == null)
                throw new ArgumentNullException("Id is not found");

            var entry = Table.Remove(element);
            return entry.State == EntityState.Deleted;
        }

        #endregion


        #region Update

        public bool Update(T entity)
        {
            var entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(string Id)
        {
            var element = await Table.FindAsync(Id);

            if (element == null)
                throw new ArgumentNullException("Id is not found");

            var entry = Table.Update(element);
            return entry.State == EntityState.Modified;
        }

        #endregion


        #region Save

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        #endregion
    }
}
