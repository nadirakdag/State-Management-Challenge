using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> Get();
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> Get(Guid id);
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task<T> Delete(Guid id);
    }
}