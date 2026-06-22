namespace ToDoList.Domain.ValueObjects;

public readonly record struct UserId(string Value)
{
    public static UserId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("User id is required.", nameof(value));
        }

        return new UserId(value.Trim());
    }

    public override string ToString()
    {
        return Value;
    }
}