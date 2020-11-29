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
        private readonly IUnitOfWork _unitOfWork;

        public FlowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<Flow>> Get()
        {
            return await _unitOfWork.FlowRepository.Get();
        }

        public async Task<Flow> Get(Guid id)
        {
            return await _unitOfWork.FlowRepository.Get(id);
        }

        public async Task<Flow> Update(Flow flow)
        {
            if (await _unitOfWork.FlowRepository.IsAny(flow.Id) == false)
            {
                return null;
            }
            
            flow = _unitOfWork.FlowRepository.Update(flow);
            await _unitOfWork.SaveChangesAsync();
            return flow;
        }

        public async Task<Flow> Create(Flow flow)
        {
            flow.Id = Guid.NewGuid();
            flow = await _unitOfWork.FlowRepository.Create(flow);
            await _unitOfWork.SaveChangesAsync();
            return flow;
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.FlowRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}