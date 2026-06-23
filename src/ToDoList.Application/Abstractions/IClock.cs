namespace ToDoList.Application.Abstractions;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}