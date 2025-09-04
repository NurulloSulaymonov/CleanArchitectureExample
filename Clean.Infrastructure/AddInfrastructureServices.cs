using Clean.Application.Abstractions;
using Clean.Domain.Abstractions;
using Clean.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure;

public static class AddInfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");
        services.AddDbContext<IAppDbContext,AppDbContext>(opt => opt.UseNpgsql(connectionString));
        
        return services;
    }
}