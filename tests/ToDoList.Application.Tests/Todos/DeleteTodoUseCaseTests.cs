using ToDoList.Application.Common;
using ToDoList.Application.Tests.Fakes;
using ToDoList.Application.Todos;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Todos;

public sealed class DeleteTodoUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenTodoBelongsToCurrentUser_DeletesTodo()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var useCase = new DeleteTodoUseCase(repository, currentUser);

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

        Assert.Null(storedTodo);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTodoBelongsToAnotherUser_ReturnsNotFoundAndDoesNotDeleteTodo()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var useCase = new DeleteTodoUseCase(repository, currentUser);

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

        var stillExistingTodo = await repository.GetByIdAsync(
            otherUserTodo.Id,
            UserId.Create("user-2"),
            CancellationToken.None);

        Assert.NotNull(stillExistingTodo);
    }
}