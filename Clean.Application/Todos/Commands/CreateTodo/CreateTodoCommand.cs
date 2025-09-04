using Clean.Application.Abstractions;
using Clean.Application.Common.Results;
using Clean.Domain.Entities;
using MediatR;

namespace Clean.Application.Todos.Commands.CreateTodo;

public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate) : IRequest<Result<Guid>>;


public class CreateTodoCommandHandler(IAppDbContext db) : IRequestHandler<CreateTodoCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTodoCommand request, CancellationToken ct)
    {
        var entity = new Todo(request.Title, request.Description, request.DueDate);
        await db.Todos.AddAsync(entity, ct);
        await db.SaveChangesAsync(ct);
        return Result<Guid>.Success(entity.Id);
    }
}