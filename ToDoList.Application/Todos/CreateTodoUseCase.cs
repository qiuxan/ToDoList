using ToDoList.Application.Abstractions;
using ToDoList.Application.Common;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Todos;

public sealed record CreateTodoCommand(
    string Title,
    string? Description);

public sealed class CreateTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public CreateTodoUseCase(
        ITodoRepository todoRepository,
        ICurrentUser currentUser,
        IClock clock)
    {
        _todoRepository = todoRepository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<Result<TodoDto>> ExecuteAsync(
        CreateTodoCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var todo = TodoItem.Create(
                command.Title,
                command.Description,
                _currentUser.UserId,
                _clock.UtcNow);

            await _todoRepository.AddAsync(todo, cancellationToken);

            return Result<TodoDto>.Success(TodoDto.FromTodoItem(todo));
        }
        catch (ArgumentException exception)
        {
            return Result<TodoDto>.ValidationError(exception.Message);
        }
    }
}