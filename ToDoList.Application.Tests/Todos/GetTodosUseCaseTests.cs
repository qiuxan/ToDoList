using ToDoList.Application.Tests.Fakes;
using ToDoList.Application.Todos;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Todos;

public sealed class GetTodosUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsOnlyTodosForCurrentUser()
    {
        var repository = new FakeTodoRepository();
        var currentUser = new FakeCurrentUser(UserId.Create("user-1"));
        var useCase = new GetTodosUseCase(repository, currentUser);

        var userTodo = TodoItem.Create(
            "User todo",
            null,
            UserId.Create("user-1"),
            new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        var otherUserTodo = TodoItem.Create(
            "Other user todo",
            null,
            UserId.Create("user-2"),
            new DateTimeOffset(2026, 6, 23, 10, 0, 0, TimeSpan.Zero));

        repository.Seed(userTodo);
        repository.Seed(otherUserTodo);

        var result = await useCase.ExecuteAsync(CancellationToken.None);

        var todo = Assert.Single(result);
        Assert.Equal(userTodo.Id, todo.Id);
        Assert.Equal("User todo", todo.Title);
    }
}