namespace Clean.Application.Dtos.Todo;

public class CreateTodoDto
{
    public Guid Id { get;  set; }
    public string Title { get;  set; }
    public string? Description { get;  set; }
    public bool IsDone { get;  set; }
    public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;
    public DateTime? DueDate { get;  set; }
}

public class GetTodoDto
{
    public Guid Id { get;  set; }
    public string Title { get;  set; }
    public string? Description { get;  set; }
    public bool IsDone { get;  set; }
    public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;
    public DateTime? DueDate { get;  set; }
}