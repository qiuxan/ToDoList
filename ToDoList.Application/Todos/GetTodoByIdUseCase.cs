using ToDoList.Application.Abstractions;
using ToDoList.Application.Common;

namespace ToDoList.Application.Todos;

public sealed class GetTodoByIdUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICurrentUser _currentUser;

    public GetTodoByIdUseCase(
        ITodoRepository todoRepository,
        ICurrentUser currentUser)
    {
        _todoRepository = todoRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<TodoDto>> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(
            id,
            _currentUser.UserId,
            cancellationToken);

        return todo is null
            ? Result<TodoDto>.NotFound()
            : Result<TodoDto>.Success(TodoDto.FromTodoItem(todo));
    }
}