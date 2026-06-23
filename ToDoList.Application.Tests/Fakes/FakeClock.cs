using ToDoList.Application.Abstractions;

namespace ToDoList.Application.Tests.Fakes;

public sealed class FakeClock : IClock
{
    public FakeClock(DateTimeOffset utcNow)
    {
        UtcNow = utcNow;
    }

    public DateTimeOffset UtcNow { get; set; }
}