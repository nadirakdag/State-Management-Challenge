using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Application.Services;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        [Test]
        public async Task When_Delete_Middle_State_Should_Update_PrevState_And_NextState()
        {
            Guid gonnaDeleted = Guid.NewGuid();
            Guid prevStateId = Guid.NewGuid();
            Guid nextStateId = Guid.NewGuid();

            List<State> states = new List<State>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(s => s.StateRepository.Get(It.Is<Guid>(x => x == gonnaDeleted)))
                .Returns(Task.FromResult(new State()
                {
                    Id = gonnaDeleted,
                    NextStateId = nextStateId,
                    PrevStateId = prevStateId
                }));

            mockUnitOfWork.Setup(s => s.StateRepository.Get(It.Is<Guid>(x => x == prevStateId)))
                .Returns(Task.FromResult(new State()
                {
                    Id = prevStateId,
                    NextStateId = gonnaDeleted,
                    PrevStateId = null
                }));

            mockUnitOfWork.Setup(s => s.StateRepository.Get(It.Is<Guid>(x => x == nextStateId)))
                .Returns(Task.FromResult(new State()
                {
                    Id = nextStateId,
                    NextStateId = null,
                    PrevStateId = gonnaDeleted
                }));

            mockUnitOfWork.Setup(s => s.StateRepository.Delete(It.Is<Guid>(x => x == gonnaDeleted)));


            mockUnitOfWork.Setup(s => s.StateRepository.Update(It.IsAny<State>()))
                .Callback<State>(state => states.Add(state));

            var stateService = new StateService(mockUnitOfWork.Object);

            await stateService.Delete(gonnaDeleted);

            var prevState = states.First(x => x.Id == prevStateId);
            Assert.AreEqual(nextStateId, prevState.NextStateId);

            var nextState = states.First(x => x.Id == nextStateId);
            Assert.AreEqual(prevStateId, nextState.PrevStateId);
        }

        [Test]
        public async Task
            When_Create_New_State_If_Already_State_Exist_In_Flow_Should_Update_Last_States_NextState_Prop()
        {
            Guid updatedState = Guid.NewGuid();
            Guid flowId = Guid.NewGuid();

            List<State> states = new List<State>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(s => s.StateRepository.GetLastStateByFlowId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new State()
                {
                    Id = updatedState,
                    FlowId = flowId,
                    NextStateId = null,
                    PrevStateId = null
                }));


            mockUnitOfWork.Setup(s => s.StateRepository.Update(It.IsAny<State>()))
                .Callback<State>(state => states.Add(state));

            mockUnitOfWork.Setup(s => s.StateRepository.Create(It.IsAny<State>()))
                .Callback<State>(state => states.Add(state)).Returns((State x) => Task.FromResult(x));

            var stateService = new StateService(mockUnitOfWork.Object);

            var newState = await stateService.Create(new State()
            {
                FlowId = flowId
            });

            var prevState = states.First(x => x.Id == updatedState);
            Assert.AreEqual(newState.Id, prevState.NextStateId);

            var nextState = states.First(x => x.Id == newState.Id);
            Assert.AreEqual(updatedState, nextState.PrevStateId);
        }
    }
}