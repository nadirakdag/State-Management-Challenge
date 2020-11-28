using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Services
{
    public interface IStateService
    {
        Task<List<State>> GetByFlowId(Guid flowId);
        Task<State> Get(Guid id);
        Task<State> Update(State state);
        Task<State> Create(State state);
        Task Delete(Guid id);
    }
}