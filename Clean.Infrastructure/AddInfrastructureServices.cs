using Clean.Application.Abstractions;
using Clean.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure;

public static class AddInfrastructureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultDemo");
        services.AddTransient<ITodoRepository, DapperTodoRepository>(options=>new DapperTodoRepository(connectionString));
        services.AddDbContext<IAppDbContext,AppDbContext>(opt => opt.UseNpgsql(connectionString));
    }
}