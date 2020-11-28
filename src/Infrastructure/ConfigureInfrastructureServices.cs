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

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            return services;
        }
    }
}