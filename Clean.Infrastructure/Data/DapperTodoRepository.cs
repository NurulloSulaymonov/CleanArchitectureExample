using Clean.Application.Abstractions;
using Clean.Domain.Entities;
using Dapper;
using Npgsql;

namespace Clean.Infrastructure.Data;

public class DapperTodoRepository : ITodoRepository
{
    private readonly string _connectionString;

    public DapperTodoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<Todo> GetAll()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var todos = connection.Query<Todo>("SELECT id, title, is_completed FROM todos");
            return todos.AsList();
        }
    }
}