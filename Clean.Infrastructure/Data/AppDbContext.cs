using Clean.Application.Abstractions;
using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Todo> Todos => Set<Todo>();

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
       return base.SaveChangesAsync(ct);
    }

    public async Task MigrateAsync()
    {
        await Database.MigrateAsync();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}