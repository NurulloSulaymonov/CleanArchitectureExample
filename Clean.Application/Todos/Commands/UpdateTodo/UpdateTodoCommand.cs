using Clean.Application.Abstractions;
using Clean.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(Guid Id, string Title, string? Description, DateTime? DueDate, bool IsDone) : IRequest<Result>;


public class UpdateTodoCommandHandler(IAppDbContext db) : IRequestHandler<UpdateTodoCommand, Result>
{
    public async Task<Result> Handle(UpdateTodoCommand request, CancellationToken ct)
    {
        var todo = await db.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, ct);
        if (todo is null) return Result.Failure("Todo not found");
        todo.Update(request.Title, request.Description, request.DueDate);
        if (request.IsDone) todo.MarkDone();
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }
}