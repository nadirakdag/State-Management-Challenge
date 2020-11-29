using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Services
{
    public interface ITaskService
    {
        Task<List<StateTask>> Get();
        Task<List<StateTask>> GetTasksByFlowId(Guid flowId);
        Task<List<StateTask>> GetTasksByStateId(Guid stateId); 
        Task<StateTask> Get(Guid id);
        Task<StateTask> Update(StateTask task);
        Task<StateTask> Create(StateTask task);
        Task Delete(Guid id);
        Task<StateTask> ToNextStage(Guid taskId);
        Task<StateTask> ToPrevStage(Guid taskId);
    }
}