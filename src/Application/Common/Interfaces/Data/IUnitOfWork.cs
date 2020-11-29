using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IFlowRepository FlowRepository { get; }
        IStateRepository StateRepository { get; }
        ITaskRepository TaskRepository { get; } 

        Task<int> SaveChangesAsync();
    }
}