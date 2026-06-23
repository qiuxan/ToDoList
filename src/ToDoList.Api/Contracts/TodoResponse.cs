using ToDoList.Application.Todos;

namespace ToDoList.Api.Contracts;

public sealed record TodoResponse(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt)
{
    public static TodoResponse FromTodoDto(TodoDto todo)
    {
        return new TodoResponse(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt,
            todo.UpdatedAt);
    }
}