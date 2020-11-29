using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
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
            return await _unitOfWork.TaskRepository.GetTasksByFlowId(flowId);
        }

        public async Task<List<StateTask>> GetTasksByStateId(Guid stateId)
        {
            return await _unitOfWork.TaskRepository.GetTasksByStateId(stateId);
        }

        public async Task<StateTask> Get(Guid id)
        {
            return await _unitOfWork.TaskRepository.Get(id);
        }

        public async Task<StateTask> Update(StateTask task)
        {
            var gonnaUpdateTask = await _unitOfWork.TaskRepository.Get(task.Id);
            if (gonnaUpdateTask == null)
                return null;

            gonnaUpdateTask.Title = task.Title;

            gonnaUpdateTask = _unitOfWork.TaskRepository.Update(gonnaUpdateTask);
            await _unitOfWork.SaveChangesAsync();
            return gonnaUpdateTask;
        }

        public async Task<StateTask> Create(StateTask task)
        {
            var firstState = await _unitOfWork.StateRepository.GetFirstStateByFlowId(task.FlowId);

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
            var task = await _unitOfWork.TaskRepository.Get(taskId);
            if (task == null)
            {
                return null;
            }

            var currentSate = task.State;
            if (currentSate.NextStateId == null)
            {
                throw new TaskStateUpdateException("No next state",task);
            }

            task.StateId = task.State.NextStateId.Value;

            _unitOfWork.TaskRepository.Update(task);

            await _unitOfWork.SaveChangesAsync();

            task = await Get(taskId);
            return task;
        }

        public async Task<StateTask> ToPrevStage(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.Get(taskId);
            if (task == null)
            {
                return null;
            }

            var currentSate = task.State;
            if (currentSate.PrevStateId == null)
            {
                throw new TaskStateUpdateException("No prev state", task);
            }

            task.StateId = task.State.PrevStateId.Value;

            _unitOfWork.TaskRepository.Update(task);

            await _unitOfWork.SaveChangesAsync();

            task = await Get(taskId);
            return task;
        }
    }
}