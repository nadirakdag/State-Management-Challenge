using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StateManagementContext : DbContext
    {
        public StateManagementContext(DbContextOptions<StateManagementContext> options) : base(options)
        {
            
        }
        
        public DbSet<Flow> Flows { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}