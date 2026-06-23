using ToDoList.Application.Abstractions;
using ToDoList.Application.Common;

namespace ToDoList.Application.Todos;

public sealed record UpdateTodoCommand(
    string Title,
    string? Description);

public sealed class UpdateTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public UpdateTodoUseCase(
        ITodoRepository todoRepository,
        ICurrentUser currentUser,
        IClock clock)
    {
        _todoRepository = todoRepository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<Result<TodoDto>> ExecuteAsync(
        Guid id,
        UpdateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(
            id,
            _currentUser.UserId,
            cancellationToken);

        if (todo is null)
        {
            return Result<TodoDto>.NotFound();
        }

        try
        {
            todo.UpdateDetails(
                command.Title,
                command.Description,
                _clock.UtcNow);

            await _todoRepository.UpdateAsync(todo, cancellationToken);

            return Result<TodoDto>.Success(TodoDto.FromTodoItem(todo));
        }
        catch (ArgumentException exception)
        {
            return Result<TodoDto>.ValidationError(exception.Message);
        }
    }
}