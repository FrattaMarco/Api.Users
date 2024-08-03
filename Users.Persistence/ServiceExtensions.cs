using DapperContext.Application.Repositories;
using DapperContext.Application.UnitOfWork;
using DapperContext.Persistence.Context;
using DapperContext.Persistence.Repositories;
using DapperContext.Persistence.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Repositories;
using Users.Persistence.Repositories;

namespace Users.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration conf)
        {
            string connectionString = conf.GetConnectionString("ConnectionStringUsers") ?? throw new ArgumentNullException(nameof(conf));
            services.AddScoped(_ => new ContextDapper(connectionString));
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(s => new UnitOfWork(s.GetRequiredService<ContextDapper>()));
        }
    }
}