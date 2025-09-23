using Clean.Application.Abstractions;
using Clean.Domain.Entities;
using Dapper;
using Npgsql;

namespace Clean.Infrastructure.Data;

public class NpgsqlTodoRepository : ITodoRepository
{
    private readonly string _connectionString;

    public NpgsqlTodoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Todo> GetAll()
    {
        var todos = new List<Todo>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand("SELECT id, title, is_completed FROM todos", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var todo = new Todo()
                    {
                        Id = (Guid)reader.GetValue(0),                 // column index 0 = id
                        Title = reader.GetString(1),             // column index 1 = title
                        IsDone = reader.GetBoolean(2)      // column index 2 = is_completed
                    };
                    todos.Add(todo);
                }
            }
        }

        return todos;
    }
    
    // ....
}
