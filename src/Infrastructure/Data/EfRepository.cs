using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly StateManagementContext StateManagementContext;

        protected EfRepository(StateManagementContext stateManagementContext)
        {
            this.StateManagementContext = stateManagementContext;
        }

        public async Task<List<T>> Get()
        {
            return await StateManagementContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> Get(Guid id)
        {
            return await StateManagementContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Create(T model)
        {
            await StateManagementContext.Set<T>().AddAsync(model);
            return model;
        }

        public T Update(T model)
        {
            StateManagementContext.Set<T>().Update(model);
            return model;
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await Get(id);
            if (entity == null)
                return null;

            StateManagementContext.Set<T>().Remove(entity);
            return entity;
        }
    }
}