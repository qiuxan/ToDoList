using ToDoList.Domain.Entities;

namespace ToDoList.Application.Todos;

public sealed record TodoDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt)
{
    public static TodoDto FromTodoItem(TodoItem todo)
    {
        return new TodoDto(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt,
            todo.UpdatedAt);
    }
}