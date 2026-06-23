using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Abstractions;

public interface ITodoRepository
{
    Task<IReadOnlyCollection<TodoItem>> GetAllAsync(
        UserId userId,
        CancellationToken cancellationToken);

    Task<TodoItem?> GetByIdAsync(
        Guid id,
        UserId userId,
        CancellationToken cancellationToken);

    Task AddAsync(
        TodoItem todo,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        TodoItem todo,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        UserId userId,
        CancellationToken cancellationToken);
}