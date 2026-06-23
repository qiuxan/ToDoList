using ToDoList.Application.Common;
using ToDoList.Application.Tests.Fakes;
using ToDoList.Application.Todos;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Todos;

public sealed class UpdateTodoUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenTodoExists_UpdatesTitleDescriptionAndUpdatedAt()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 11, 0, 0, TimeSpan.Zero));

        var useCase = new UpdateTodoUseCase(
            repository,
            currentUser,
            clock);

        var todo = TodoItem.Create(
            "Old title",
            "Old description",
            currentUser.UserId,
            new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        repository.Seed(todo);

        var result = await useCase.ExecuteAsync(
            todo.Id,
            new UpdateTodoCommand("  New title  ", "  New description  "),
            CancellationToken.None);

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal("New title", result.Value.Title);
        Assert.Equal("New description", result.Value.Description);
        Assert.Equal(clock.UtcNow, result.Value.UpdatedAt);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTodoBelongsToAnotherUser_ReturnsNotFound()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 11, 0, 0, TimeSpan.Zero));

        var useCase = new UpdateTodoUseCase(
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
            new UpdateTodoCommand("New title", null),
            CancellationToken.None);

        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_WithBlankTitle_ReturnsValidationError()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var clock = new FakeClock(new DateTimeOffset(2026, 6, 23, 11, 0, 0, TimeSpan.Zero));

        var useCase = new UpdateTodoUseCase(
            repository,
            currentUser,
            clock);

        var todo = TodoItem.Create(
            "Old title",
            null,
            currentUser.UserId,
            new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        repository.Seed(todo);

        var result = await useCase.ExecuteAsync(
            todo.Id,
            new UpdateTodoCommand("   ", null),
            CancellationToken.None);

        Assert.Equal(ResultStatus.ValidationError, result.Status);
        Assert.Null(result.Value);
        Assert.Contains(result.Errors, error => error.Contains("Todo title is required."));
    }
}