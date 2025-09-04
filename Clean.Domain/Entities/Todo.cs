namespace Clean.Domain.Entities;

public class Todo
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsDone { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; private set; }


    private Todo() { }
    public Todo(string title, string? description = null, DateTime? dueDate = null)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
        Title = title.Trim();
        Description = description?.Trim();
        DueDate = dueDate;
    }


    public void MarkDone() => IsDone = true;
    public void Update(string title, string? description, DateTime? dueDate)
    {
        if (!string.IsNullOrWhiteSpace(title)) Title = title.Trim();
        Description = description?.Trim();
        DueDate = dueDate;
    }
}