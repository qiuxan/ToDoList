using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Abstractions;
using ToDoList.Infrastructure.Identity;
using ToDoList.Infrastructure.Persistence;
using ToDoList.Infrastructure.Time;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
        services.AddScoped<ICurrentUser, DefaultCurrentUser>();
        services.AddSingleton<IClock, SystemClock>();

        return services;
    }
}