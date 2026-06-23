using ToDoList.Application.Abstractions;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Tests.Fakes;

public sealed class FakeCurrentUser : ICurrentUser
{
    public FakeCurrentUser(UserId userId)
    {
        UserId = userId;
    }

    public UserId UserId { get; set; }
}