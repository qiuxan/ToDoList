using ToDoList.Application.Abstractions;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Fakes;

public sealed class FakeTodoRepository : ITodoRepository
{
    private readonly Dictionary<Guid, TodoItem> _todos = new();

    public Task<IReadOnlyCollection<TodoItem>> GetAllAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var todos = _todos.Values
            .Where(todo => todo.UserId == userId)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<TodoItem>>(todos);
    }

    public Task<TodoItem?> GetByIdAsync(
        Guid id,
        UserId userId,
        CancellationToken cancellationToken)
    {
        _todos.TryGetValue(id, out var todo);

        return Task.FromResult(
            todo is not null && todo.UserId == userId
                ? todo
                : null);
    }

    public Task AddAsync(
        TodoItem todo,
        CancellationToken cancellationToken)
    {
        _todos.Add(todo.Id, todo);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(
        TodoItem todo,
        CancellationToken cancellationToken)
    {
        _todos[todo.Id] = todo;

        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(
        Guid id,
        UserId userId,
        CancellationToken cancellationToken)
    {
        var todo = _todos.GetValueOrDefault(id);

        if (todo is null || todo.UserId != userId)
        {
            return Task.FromResult(false);
        }

        _todos.Remove(id);

        return Task.FromResult(true);
    }

    public void Seed(TodoItem todo)
    {
        _todos.Add(todo.Id, todo);
    }
}