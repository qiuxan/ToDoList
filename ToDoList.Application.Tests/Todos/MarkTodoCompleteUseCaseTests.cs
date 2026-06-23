using ToDoList.Application.Common;
using ToDoList.Application.Tests.Fakes;
using ToDoList.Application.Todos;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Todos;

public sealed class MarkTodoCompleteUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenTodoExists_MarksTodoCompleteAndUpdatesUpdatedAt()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 11, 0, 0, TimeSpan.Zero));

        var useCase = new MarkTodoCompleteUseCase(
            repository,
            currentUser,
            clock);

        var todo = TodoItem.Create(
            "Todo",
            null,
            currentUser.UserId,
            new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        repository.Seed(todo);

        var result = await useCase.ExecuteAsync(todo.Id, CancellationToken.None);

        Assert.Equal(ResultStatus.Success, result.Status);

        var storedTodo = await repository.GetByIdAsync(
            todo.Id,
            currentUser.UserId,
            CancellationToken.None);

        Assert.NotNull(storedTodo);
        Assert.True(storedTodo.IsCompleted);
        Assert.Equal(clock.UtcNow, storedTodo.UpdatedAt);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTodoBelongsToAnotherUser_ReturnsNotFound()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 11, 0, 0, TimeSpan.Zero));

        var useCase = new MarkTodoCompleteUseCase(
            repository,
            currentUser,
            clock);

        var otherUserTodo = TodoItem.Create(
            "Other user todo",
            null,
            UserId.Create("user-2"),
            new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        repository.Seed(otherUserTodo);

        var result = await useCase.ExecuteAsync(
            otherUserTodo.Id,
            CancellationToken.None);

        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}