using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IRepository<Flow> FlowRepository { get; }
        IRepository<State> StateRepository { get; set; }

        Task<int> SaveChangesAsync();
    }
}