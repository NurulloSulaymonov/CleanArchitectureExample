using Clean.Application.Dtos.Todo;

namespace Clean.Application.Services.Todo;

public interface ITodoService
{
    public List<Domain.Entities.Todo> Get();
    public Task<GetTodoDto> AddTodo(CreateTodoDto model);
    public Task<GetTodoDto> Update(CreateTodoDto model);
    public Task<bool> Delete(Guid id);

}


public interface IEmailService
{
    public Task<bool> Send(string data);
    
}

public class EmailService : IEmailService
{
    public async Task<bool> Send(string data)
    {
        return await Task.FromResult(true);
    }
}