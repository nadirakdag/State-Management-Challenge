using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskRepository : EfRepository<StateTask>, ITaskRepository
    {
        public TaskRepository(StateManagementContext stateManagementContext) 
            : base (stateManagementContext)
        {
                
        }

        public override async Task<StateTask> Get(Guid id)
        {
            return await StateManagementContext
                .Set<StateTask>()
                .Include(x => x.Flow)
                .Include(x => x.State)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<StateTask>> GetTasksByFlowId(Guid flowId)
        {
            return await StateManagementContext
                .Set<StateTask>()
                .Include(x => x.Flow)
                .Include(x => x.State)
                .Where(x => x.FlowId == flowId)
                .ToListAsync();
        }

        public async Task<List<StateTask>> GetTasksByStateId(Guid stateId)
        {
            return await StateManagementContext
                .Set<StateTask>()
                .Include(x => x.Flow)
                .Include(x => x.State)
                .Where(x => x.StateId == stateId)
                .ToListAsync();
        }
    }
}