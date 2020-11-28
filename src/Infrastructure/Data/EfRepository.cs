using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly StateManagementContext _stateManagementContext;

        public EfRepository(StateManagementContext stateManagementContext)
        {
            _stateManagementContext = stateManagementContext;
        }

        public async Task<List<T>> Get()
        {
            return await _stateManagementContext.Set<T>().ToListAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _stateManagementContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _stateManagementContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await _stateManagementContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Create(T model)
        {
            await _stateManagementContext.Set<T>().AddAsync(model);
            await _stateManagementContext.SaveChangesAsync();
            return model;
        }

        public async Task<T> Update(T model)
        {
            _stateManagementContext.Set<T>().Update(model);
            var effectedRecordCount = await _stateManagementContext.SaveChangesAsync();
            if (effectedRecordCount < 1)
                return null;

            return model;
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await Get(id);
            if (entity == null)
                return null;

            _stateManagementContext.Set<T>().Remove(entity);
            await _stateManagementContext.SaveChangesAsync();
            return entity;
        }
    }
}