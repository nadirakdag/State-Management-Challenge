using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Application.Common.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StateTask>> Get()
        {
            return await _unitOfWork.TaskRepository.Get();
        }

        public async Task<List<StateTask>> GetTasksByFlowId(Guid flowId)
        {
            return await _unitOfWork.TaskRepository.Get(x => x.FlowId == flowId);
        }

        public async Task<List<StateTask>> GetTasksByStateId(Guid stateId)
        {
            return await _unitOfWork.TaskRepository.Get(x => x.StateId == stateId);
        }

        public async Task<StateTask> Get(Guid id)
        {
            return await _unitOfWork.TaskRepository.FirstOrDefault(x => x.Id == id);
        }

        public async Task<StateTask> Update(StateTask task)
        {
            task = _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
            return task;
        }

        public async Task<StateTask> Create(StateTask task)
        {
            var firstState =
                await _unitOfWork.StateRepository.FirstOrDefault(x => x.FlowId == task.FlowId && x.PrevStateId == null);
            task.Id = Guid.NewGuid();
            task.StateId = firstState.Id;

            await _unitOfWork.TaskRepository.Create(task);
            await _unitOfWork.SaveChangesAsync();

            return task;
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.TaskRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<StateTask> ToNextStage(Guid taskId)
        {
            
            var task = await _unitOfWork.TaskRepository.FirstOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                return null;
            }

            var currentSate = task.State;
            if (currentSate.NextStateId == null)
            {
                throw new Exception("No next state");
            }

            task.StateId = task.State.NextStateId.Value;
            
            _unitOfWork.TaskRepository.Update(task);
            
            await _unitOfWork.SaveChangesAsync();
            return task;
        }

        public async Task<StateTask> ToPrevStage(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.FirstOrDefault(x => x.Id == taskId);
            if (task == null)
            {
                return null;
            }

            var currentSate = task.State;
            if (currentSate.PrevStateId == null)
            {
                throw new Exception("No next state");
            }

            task.StateId = task.State.PrevStateId.Value;
            
            _unitOfWork.TaskRepository.Update(task);
            
            await _unitOfWork.SaveChangesAsync();
            return task;
        }
    }
}