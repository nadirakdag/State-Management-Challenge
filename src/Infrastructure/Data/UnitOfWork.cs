using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IFlowRepository flowRepository,
            IStateRepository stateRepository,
            ITaskRepository taskRepository,
            StateManagementContext stateManagementContext)
        {
            FlowRepository = flowRepository;
            StateRepository = stateRepository;
            _stateManagementContext = stateManagementContext;
            TaskRepository = taskRepository;
        }

        public IFlowRepository FlowRepository { get; }
        public IStateRepository StateRepository { get; }
        public ITaskRepository TaskRepository { get; }

        private readonly StateManagementContext _stateManagementContext;

        public async Task<int> SaveChangesAsync()
        {
            return await _stateManagementContext.SaveChangesAsync();
        }
    }
}