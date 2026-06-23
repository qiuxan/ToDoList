namespace ToDoList.Api.Contracts;

public sealed record UpdateTodoRequest(
    string Title,
    string? Description);