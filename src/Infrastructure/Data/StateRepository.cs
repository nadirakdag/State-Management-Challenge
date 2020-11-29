using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StateRepository : EfRepository<State>, IStateRepository
    {
        public StateRepository(StateManagementContext stateManagementContext)
            : base(stateManagementContext)
        {
        }

        public async Task<List<State>> GetByFlowId(Guid flowId)
        {
            return await StateManagementContext
                .Set<State>()
                .Include(x => x.Flow)
                .Where(x => x.FlowId == flowId)
                .ToListAsync();
        }

        public async Task<State> GetLastStateByFlowId(Guid flowId)
        {
            return await StateManagementContext
                .Set<State>()
                .FirstOrDefaultAsync(x => x.FlowId == flowId && x.NextStateId == null);
        }

        public async Task<State> GetFirstStateByFlowId(Guid flowId)
        {
            return await StateManagementContext
                .Set<State>()
                .FirstOrDefaultAsync(x => x.FlowId == flowId && x.PrevStateId == null);
        }
    }
}