using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Services
{
    public interface IFlowService
    {
        Task<List<Flow>> Get();
        Task<Flow> Get(Guid id);
        Task<Flow> Update(Flow flow);
        Task<Flow> Create(Flow flow);
        Task Delete(Guid id);
    }
}