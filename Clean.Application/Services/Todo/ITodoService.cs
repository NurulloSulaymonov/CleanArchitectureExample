using Clean.Application.Dtos.Todo;

namespace Clean.Application.Services.Todo;

public interface ITodoService
{
    public List<Domain.Entities.Todo> Get();

    public Task<GetTodoDto> AddTodo(CreateTodoDto model);

}