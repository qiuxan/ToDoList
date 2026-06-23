using ToDoList.Application.Abstractions;
using ToDoList.Application.Common;

namespace ToDoList.Application.Todos;

public sealed class DeleteTodoUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICurrentUser _currentUser;

    public DeleteTodoUseCase(
        ITodoRepository todoRepository,
        ICurrentUser currentUser)
    {
        _todoRepository = todoRepository;
        _currentUser = currentUser;
    }

    public async Task<Result> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _todoRepository.DeleteAsync(
            id,
            _currentUser.UserId,
            cancellationToken);

        return deleted
            ? Result.Success()
            : Result.NotFound();
    }
}