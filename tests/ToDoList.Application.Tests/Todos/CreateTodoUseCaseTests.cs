using ToDoList.Application.Common;
using ToDoList.Application.Tests.Fakes;
using ToDoList.Application.Todos;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Todos;

public sealed class CreateTodoUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidTitle_CreatesTodoForCurrentUser()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        var useCase = new CreateTodoUseCase(
            repository,
            currentUser,
            clock);

        var result = await useCase.ExecuteAsync(
            new CreateTodoCommand("  Buy milk  ", "  Full cream  "),
            CancellationToken.None);

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal("Buy milk", result.Value.Title);
        Assert.Equal("Full cream", result.Value.Description);
        Assert.False(result.Value.IsCompleted);
        Assert.Equal(clock.UtcNow, result.Value.CreatedAt);
        Assert.Equal(clock.UtcNow, result.Value.UpdatedAt);

        var storedTodo = await repository.GetByIdAsync(
            result.Value.Id,
            currentUser.UserId,
            CancellationToken.None);

        Assert.NotNull(storedTodo);
        Assert.Equal(currentUser.UserId, storedTodo.UserId);
    }

    [Fact]
    public async Task ExecuteAsync_WithBlankTitle_ReturnsValidationError()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        var useCase = new CreateTodoUseCase(
            repository,
            currentUser,
            clock);

        var result = await useCase.ExecuteAsync(
            new CreateTodoCommand("   ", null),
            CancellationToken.None);

        Assert.Equal(ResultStatus.ValidationError, result.Status);
        Assert.Null(result.Value);
        Assert.Contains(result.Errors, error => error.Contains("Todo title is required."));

        var todos = await repository.GetAllAsync(
            currentUser.UserId,
            CancellationToken.None);

        Assert.Empty(todos);
    }
}