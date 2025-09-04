using System.Reflection;
using Clean.Application.Common.Behaviours;
using Clean.Application.Todos.Commands.CreateTodo;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application;

public static class AddApplicationServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
       
        services.AddValidatorsFromAssembly(typeof(CreateTodoCommand).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateTodoCommand).Assembly);
            //cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            
        });
        
        return services;
    }
}