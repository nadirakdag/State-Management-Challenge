using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> Get();
        Task<T> Get(Guid id);
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task<T> Delete(Guid id);
    }
}