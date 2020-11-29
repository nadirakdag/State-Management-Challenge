using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<Flow> flowRepository, IRepository<State> stateRepository, StateManagementContext stateManagementContext)
        {
            FlowRepository = flowRepository;
            StateRepository = stateRepository;
            _stateManagementContext = stateManagementContext;
        }

        public IRepository<Flow> FlowRepository { get; }
        public IRepository<State> StateRepository { get; set; }
        
        private readonly StateManagementContext _stateManagementContext; 
        
        public async Task<int> SaveChangesAsync()
        {
            return await _stateManagementContext.SaveChangesAsync();
        }
    }
}