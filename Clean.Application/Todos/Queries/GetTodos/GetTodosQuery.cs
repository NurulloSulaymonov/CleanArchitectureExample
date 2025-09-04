using Clean.Application.Abstractions;
using Clean.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Todos.Queries.GetTodos;

public record GetTodosQuery(bool? IsDone = null) : IRequest<Result<List<TodoDto>>>;


public record TodoDto(Guid Id, string Title, string? Description, bool IsDone, DateTime CreatedAt, DateTime? DueDate);


public class GetTodosQueryHandler(IAppDbContext db) : IRequestHandler<GetTodosQuery, Result<List<TodoDto>>>
{
    public async Task<Result<List<TodoDto>>> Handle(GetTodosQuery request, CancellationToken ct)
    {
        var query = db.Todos.AsNoTracking().AsQueryable();
        if (request.IsDone.HasValue) query = query.Where(x => x.IsDone == request.IsDone);


        var list = await query
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new TodoDto(x.Id, x.Title, x.Description, x.IsDone, x.CreatedAt, x.DueDate))
            .ToListAsync(ct);
        
        return Result<List<TodoDto>>.Success(list);
    }
}