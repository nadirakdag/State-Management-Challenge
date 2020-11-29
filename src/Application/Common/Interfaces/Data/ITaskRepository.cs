using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface ITaskRepository: IRepository<StateTask>
    {
        Task<List<StateTask>> GetTasksByFlowId(Guid flowId);
        Task<List<StateTask>> GetTasksByStateId(Guid stateId);
    }
}