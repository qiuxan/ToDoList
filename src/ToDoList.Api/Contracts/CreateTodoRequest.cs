namespace ToDoList.Api.Contracts;

public sealed record CreateTodoRequest(
    string Title,
    string? Description);