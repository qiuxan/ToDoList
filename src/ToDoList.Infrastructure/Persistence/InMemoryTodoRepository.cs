using System.Collections.Concurrent;
using ToDoList.Application.Abstractions;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Infrastructure.Persistence;

public sealed class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _todos = new();

    public Task<IReadOnlyCollection<TodoItem>> GetAllAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var todos = _todos.Values
            .Where(todo => todo.UserId == userId)
            .OrderByDescending(todo => todo.CreatedAt)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<TodoItem>>(todos);
    }

    public Task<TodoItem?> GetByIdAsync(
        Guid id,
        UserId userId,
        CancellationToken cancellationToken)
    {
        if (!_todos.TryGetValue(id, out var todo))
        {
            return Task.FromResult<TodoItem?>(null);
        }

        return Task.FromResult(
            todo.UserId == userId
                ? todo
                : null);
    }

    public Task AddAsync(
        TodoItem todo,
        CancellationToken cancellationToken)
    {
        _todos[todo.Id] = todo;

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
        if (!_todos.TryGetValue(id, out var todo))
        {
            return Task.FromResult(false);
        }

        if (todo.UserId != userId)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(_todos.TryRemove(id, out _));
    }
}