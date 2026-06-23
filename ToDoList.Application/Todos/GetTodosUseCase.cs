using ToDoList.Application.Abstractions;

namespace ToDoList.Application.Todos;

public sealed class GetTodosUseCase
{
    private readonly ITodoRepository _todoRepository;
    private readonly ICurrentUser _currentUser;

    public GetTodosUseCase(
        ITodoRepository todoRepository,
        ICurrentUser currentUser)
    {
        _todoRepository = todoRepository;
        _currentUser = currentUser;
    }

    public async Task<IReadOnlyCollection<TodoDto>> ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var todos = await _todoRepository.GetAllAsync(
            _currentUser.UserId,
            cancellationToken);

        return todos
            .Select(TodoDto.FromTodoItem)
            .ToArray();
    }
}