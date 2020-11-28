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
        private readonly IRepository<State> _stateRepository;

        public StateService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<List<State>> GetByFlowId(Guid flowId)
        {
            return await _stateRepository.Get(x => x.FlowId == flowId);
        }

        public async Task<State> Get(Guid id)
        {
            return await _stateRepository.Get(id);
        }

        public async Task<State> Update(State state)
        {
            return await _stateRepository.Update(state);
        }

        public async  Task<State> Create(State state)
        {
            var prevState = await _stateRepository.FirstOrDefault(x => x.NextStateId == null);
            
            state.PrevStateId = prevState?.Id;
            state  = await _stateRepository.Create(state);
            
            if (prevState != null)
            {
                prevState.NextStateId = state.Id;
                await _stateRepository.Update(prevState);
            }
            
            return state;
        }

        public async Task Delete(Guid id)
        {
            await _stateRepository.Delete(id);
        }
    }
}