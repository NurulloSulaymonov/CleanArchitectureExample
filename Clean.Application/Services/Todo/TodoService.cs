using Clean.Application.Abstractions;
using Clean.Application.Dtos.Todo;

namespace Clean.Application.Services.Todo;

public class TodoService(IAppDbContext context) : ITodoService
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
        return new GetTodoDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsDone = false
        };
    }
}