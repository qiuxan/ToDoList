using ToDoList.Application.Abstractions;
using ToDoList.Application.Common;

namespace ToDoList.Application.Todos;

public sealed class MarkTodoCompleteUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IClock _clock;

    public MarkTodoCompleteUseCase(
        ITodoRepository todoRepository,
        ICurrentUser currentUser,
        IClock clock)
    {
        _todoRepository = todoRepository;
        _currentUser = currentUser;
        _clock = clock;
    }

    public async Task<Result> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(
            id,
            _currentUser.UserId,
            cancellationToken);

        if (todo is null)
        {
            return Result.NotFound();
        }

        todo.MarkComplete(_clock.UtcNow);

        await _todoRepository.UpdateAsync(todo, cancellationToken);

        return Result.Success();
    }
}