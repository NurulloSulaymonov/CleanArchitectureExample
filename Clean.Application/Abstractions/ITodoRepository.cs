using Clean.Domain.Entities;

namespace Clean.Application.Abstractions;

public interface ITodoRepository
{
   List<Todo> GetAll();
}