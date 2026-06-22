using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Entities;

public sealed class TodoItem
{
    private TodoItem(
        Guid id,
        string title,
        string? description,
        UserId userId,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsCompleted = false;
    }

    public Guid Id { get; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public bool IsCompleted { get; private set; }

    public UserId UserId { get; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public static TodoItem Create(
        string title,
        string? description,
        UserId userId,
        DateTimeOffset now)
    {
        return new TodoItem(
            Guid.NewGuid(),
            NormalizeTitle(title),
            NormalizeDescription(description),
            userId,
            now,
            now);
    }

    public void UpdateDetails(string title, string? description, DateTimeOffset now)
    {
        Title = NormalizeTitle(title);
        Description = NormalizeDescription(description);
        UpdatedAt = now;
    }

    public void MarkComplete(DateTimeOffset now)
    {
        IsCompleted = true;
        UpdatedAt = now;
    }

    public void MarkIncomplete(DateTimeOffset now)
    {
        IsCompleted = false;
        UpdatedAt = now;
    }

    private static string NormalizeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Todo title is required.", nameof(title));
        }

        return title.Trim();
    }

    private static string? NormalizeDescription(string? description)
    {
        return string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }
}