using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Todos;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateTodoUseCase>();
        services.AddScoped<GetTodosUseCase>();
        services.AddScoped<GetTodoByIdUseCase>();
        services.AddScoped<UpdateTodoUseCase>();
        services.AddScoped<MarkTodoCompleteUseCase>();
        services.AddScoped<MarkTodoIncompleteUseCase>();
        services.AddScoped<DeleteTodoUseCase>();

        return services;
    }
}