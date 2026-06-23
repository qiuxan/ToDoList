using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Abstractions;

public interface ICurrentUser
{
    UserId UserId { get; }
}