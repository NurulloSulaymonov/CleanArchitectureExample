using Clean.Application.Abstractions;

namespace Clean.Application.Services.Todo;

public interface ITodoService
{
    
}

public class TodoService : ITodoService
{
    private IDapperDbContext _dapper;
    public TodoService(IDapperDbContext dapper)
    {
        _dapper = dapper;
    }

    public List<Domain.Entities.Todo> Get()
    {
        // 
        throw new Exception();
    }
}