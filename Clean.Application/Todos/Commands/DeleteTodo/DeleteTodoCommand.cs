using Clean.Application.Abstractions;
using Clean.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Todos.Commands.DeleteTodo;

public record DeleteTodoCommand(Guid Id) : IRequest<Result>;


public class DeleteTodoCommandHandler(IAppDbContext db) : IRequestHandler<DeleteTodoCommand, Result>
{
    public async Task<Result> Handle(DeleteTodoCommand request, CancellationToken ct)
    {
        var todo = await db.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, ct);
        if (todo is null) return Result.Failure("Todo not found");
        db.Todos.Remove(todo);
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }
}