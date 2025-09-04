using System.Data;
using Clean.Application.Abstractions;
using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Clean.Infrastructure.Data;

public class DapperContext : IDapperDbContext
{
    private string _connnectionString;
    public DapperContext()
    {
        _connnectionString = "";
    }
    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(_connnectionString);
    }
}

