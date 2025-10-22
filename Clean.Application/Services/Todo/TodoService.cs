using Clean.Application.Abstractions;
using Clean.Application.Dtos.Todo;

namespace Clean.Application.Services.Todo;

public class TodoService(IAppDbContext context, IEmailService emailService) : ITodoService
{

    public List<Domain.Entities.Todo> Get()
    {
        return context.Todos.ToList();
    }

    public async Task<GetTodoDto> AddTodo(CreateTodoDto model)
    {
        var todo = new Domain.Entities.Todo()
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            IsDone = false
        };

        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        await emailService.Send("Hello");
        return new GetTodoDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsDone = false
        };
    }
  
    

    public async Task<GetTodoDto> Update(CreateTodoDto model)
    {
        var existingTodo = await context.Todos.FindAsync(model.Id);
        if (existingTodo == null)
        {
            throw new KeyNotFoundException($"Todo with ID {model.Id} not found");
        }
        
        existingTodo.Update(model.Title, model.Description, model.DueDate);
        
        await context.SaveChangesAsync();
        
        return new GetTodoDto
        {
            Id = existingTodo.Id,
            Title = existingTodo.Title,
            IsDone = existingTodo.IsDone,
            DueDate = existingTodo.DueDate
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var todo = await context.Todos.FindAsync(id);
        if (todo == null)
        {
            return false;
        }

        context.Todos.Remove(todo);
        var result = await context.SaveChangesAsync();
        
        return result > 0;
    }
}