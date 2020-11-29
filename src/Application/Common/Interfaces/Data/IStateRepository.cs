using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface IStateRepository : IRepository<State>
    {
        Task<List<State>> GetByFlowId(Guid flowId);
        Task<State> GetLastStateByFlowId(Guid flowId);
        Task<State> GetFirstStateByFlowId(Guid flowId);
    }
}