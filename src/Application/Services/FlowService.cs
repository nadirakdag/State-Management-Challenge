using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Application.Common.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    public class FlowService : IFlowService
    {
        private readonly IRepository<Flow> _flowRepository;

        public FlowService(IRepository<Flow> flowRepository)
        {
            _flowRepository = flowRepository;
        }
        
        public async Task<List<Flow>> Get()
        {
            return await _flowRepository.Get();
        }

        public async Task<Flow> Get(Guid id)
        {
            return await _flowRepository.Get(id);
        }

        public async Task<Flow> Update(Flow flow)
        {
            return await _flowRepository.Update(flow);
        }

        public async Task<Flow> Create(Flow flow)
        {
            return await _flowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = flow.Title
            });
        }

        public async Task Delete(Guid id)
        {
            await _flowRepository.Delete(id);
        }
    }
}