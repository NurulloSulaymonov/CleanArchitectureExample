using Clean.Application.Services.Todo;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application;

public static class AddApplicationServices
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}