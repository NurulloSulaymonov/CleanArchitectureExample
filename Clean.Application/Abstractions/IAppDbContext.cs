using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<Todo> Todos { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task MigrateAsync();
}