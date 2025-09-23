using Clean.Application.Dtos.Todo;
using Clean.Application.Services.Todo;
using Microsoft.AspNetCore.Mvc;

namespace Clean.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(ITodoService todoService) : ControllerBase
{

    [HttpGet("get")]
    public IActionResult GetAll([FromQuery] bool? isDone)
    {
        var result = todoService.Get();
        return Ok(result);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddTodo(CreateTodoDto model)
    {
        var result = await todoService.AddTodo(model);
        return Ok(result);
    }
    
}
