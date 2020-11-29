using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Application.Common.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<State>> GetByFlowId(Guid flowId)
        {
            return await _unitOfWork.StateRepository.Get(x => x.FlowId == flowId);
        }

        public async Task<State> Get(Guid id)
        {
            return await _unitOfWork.StateRepository.Get(id);
        }

        public async Task<State> Update(State state)
        {
            state =  _unitOfWork.StateRepository.Update(state);
            await _unitOfWork.SaveChangesAsync();
            return state;
        }

        public async  Task<State> Create(State state)
        {
            var prevState = await _unitOfWork.StateRepository.FirstOrDefault(x => x.NextStateId == null && x.FlowId == state.FlowId);
            
            state.Id = Guid.NewGuid();
            state.PrevStateId = prevState?.Id;
            state  = await _unitOfWork.StateRepository.Create(state);
            
            if (prevState != null)
            {
                prevState.NextStateId = state.Id;
                _unitOfWork.StateRepository.Update(prevState);
            }
            
            await _unitOfWork.SaveChangesAsync();
            return state;
        }

        public async Task Delete(Guid id)
        {
            var state = await _unitOfWork.StateRepository.Get(id);
            
            if (state.PrevStateId.HasValue)
            {
                var prevState = await _unitOfWork.StateRepository.Get(state.PrevStateId.Value);
                prevState.NextStateId = state.NextStateId;
                _unitOfWork.StateRepository.Update(prevState);
            }

            if (state.NextStateId.HasValue)
            {
                var nextState = await _unitOfWork.StateRepository.Get(state.NextStateId.Value);
                nextState.PrevStateId = state.PrevStateId;
                _unitOfWork.StateRepository.Update(nextState);
            }
            
            await _unitOfWork.StateRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}