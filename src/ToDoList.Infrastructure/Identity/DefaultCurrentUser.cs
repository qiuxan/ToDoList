using ToDoList.Application.Abstractions;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Infrastructure.Identity;

public sealed class DefaultCurrentUser : ICurrentUser
{
    public UserId UserId { get; } = UserId.Create("default-user");
}