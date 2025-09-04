using System.Data;

namespace Clean.Application.Abstractions;

public interface IDapperDbContext
{
    IDbConnection GetConnection();
}