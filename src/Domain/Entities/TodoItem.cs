namespace CleanArchitecture.Domain.Entities;

public class TodoItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;

    public void MarkComplete() => IsCompleted = true;

    public void MarkIncomplete() => IsCompleted = false;
}
