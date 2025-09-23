namespace Clean.Domain.Entities;

public class Todo
{
    
    public Guid Id { get;  set; } = Guid.NewGuid();
    public string Title { get;  set; }
    public string? Description { get;  set; }
    public bool IsDone { get;  set; }
    public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;
    public DateTime? DueDate { get;  set; }

    
    public Todo() { }
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