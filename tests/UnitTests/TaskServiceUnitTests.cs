using Application.Common.Exceptions;
using Application.Common.Interfaces.Data;
using Application.Services;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UnitTests
{
    public class TaskServiceUnitTests
    {
        [Test]
        public async Task When_Task_Wants_To_Next_State_And_It_Exit_Should_Go_Next_State()
        {
            var taskId = Guid.NewGuid();
            var taskCurrentState = Guid.NewGuid();
            var taskNextState = Guid.NewGuid();
            var flowId = Guid.NewGuid();

            StateTask task = new StateTask();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.TaskRepository.Get(It.Is<Guid>(t => t == taskId))).
                Returns(Task.FromResult(new StateTask
                {
                    FlowId = flowId,
                    StateId = taskCurrentState,
                    State = new State
                    {
                        FlowId = flowId,
                        Id = taskCurrentState,
                        NextStateId = taskNextState
                    }
                }));

            mockUnitOfWork.Setup(x => x.TaskRepository.Update(It.IsAny<StateTask>())).Callback<StateTask>((state) => task = state);

            var taskService = new TaskService(mockUnitOfWork.Object);
            await taskService.ToNextStage(taskId);

            Assert.AreEqual(taskNextState, task.StateId);
        }

        [Test]
        public void When_Task_Wants_To_Next_State_And_It_Does_Not_Exit_Should_Throw_Error()
        {
            var taskId = Guid.NewGuid();
            var taskCurrentState = Guid.NewGuid();
            var flowId = Guid.NewGuid();

            StateTask task = new StateTask();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.TaskRepository.Get(It.Is<Guid>(t => t == taskId))).
                Returns(Task.FromResult(new StateTask
                {
                    FlowId = flowId,
                    StateId = taskCurrentState,
                    State = new State
                    {
                        FlowId = flowId,
                        Id = taskCurrentState,
                        NextStateId = null
                    }
                }));

            mockUnitOfWork.Setup(x => x.TaskRepository.Update(It.IsAny<StateTask>())).Callback<StateTask>((state) => task = state);

            var taskService = new TaskService(mockUnitOfWork.Object);
            Assert.ThrowsAsync<TaskStateUpdateException>(async () => await taskService.ToNextStage(taskId));
        }

        [Test]
        public async Task When_Task_Wants_To_Prev_State_And_It_Exit_Should_Go_Next_State()
        {
            var taskId = Guid.NewGuid();
            var taskCurrentState = Guid.NewGuid();
            var taskPrevState = Guid.NewGuid();
            var flowId = Guid.NewGuid();

            StateTask task = new StateTask();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.TaskRepository.Get(It.Is<Guid>(t => t == taskId))).
                Returns(Task.FromResult(new StateTask
                {
                    FlowId = flowId,
                    StateId = taskCurrentState,
                    State = new State
                    {
                        FlowId = flowId,
                        Id = taskCurrentState,
                        NextStateId = null,
                        PrevStateId = taskPrevState
                    }
                }));

            mockUnitOfWork.Setup(x => x.TaskRepository.Update(It.IsAny<StateTask>())).Callback<StateTask>((state) => task = state);

            var taskService = new TaskService(mockUnitOfWork.Object);
            await taskService.ToPrevStage(taskId);

            Assert.AreEqual(taskPrevState, task.StateId);
        }

        [Test]
        public void When_Task_Wants_To_Prev_State_And_It_Does_Not_Exit_Should_Throw_Error()
        {
            var taskId = Guid.NewGuid();
            var taskCurrentState = Guid.NewGuid();
            var flowId = Guid.NewGuid();

            StateTask task = new StateTask();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.TaskRepository.Get(It.Is<Guid>(t => t == taskId))).
                Returns(Task.FromResult(new StateTask
                {
                    FlowId = flowId,
                    StateId = taskCurrentState,
                    State = new State
                    {
                        FlowId = flowId,
                        Id = taskCurrentState,
                        NextStateId = null,
                        PrevStateId = null
                    }
                }));

            var taskService = new TaskService(mockUnitOfWork.Object);
            Assert.ThrowsAsync<TaskStateUpdateException>(async () => await taskService.ToPrevStage(taskId));
        }
    }
}
