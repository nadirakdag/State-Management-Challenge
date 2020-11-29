using Application.Common.Interfaces.Data;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class FlowRepository : EfRepository<Flow>, IFlowRepository
    {
        public FlowRepository(StateManagementContext stateManagementContext)
            : base(stateManagementContext)
        {
        }
    }
}