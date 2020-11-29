using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IRepository<Flow> FlowRepository { get; }
        IRepository<State> StateRepository { get; }
        IRepository<StateTask> TaskRepository { get; } 

        Task<int> SaveChangesAsync();
    }
}