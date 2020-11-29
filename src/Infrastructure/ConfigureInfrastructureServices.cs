using Application.Common.Interfaces.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<StateManagementContext>(c =>
                c.UseSqlite("Filename=StateManagement.db"));

            services.AddScoped<IFlowRepository, FlowRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}