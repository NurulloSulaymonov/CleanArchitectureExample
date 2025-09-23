using Clean.Application.Services.Todo;
using Clean.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : Controller
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    [HttpGet("get-all")]
    public IActionResult Index() // action
    {
        
        return Ok(_todoService.Get());
    }
    
    [HttpGet("get-by-id")]
    public IActionResult GetbyId(int id)
    {
        return Ok();
    }
    
    [HttpPost("add-todo")]
    public IActionResult Add(Todo todo)
    {
        return Ok();
    }
    
    [HttpPost("update-todo")]
    public IActionResult Update(Todo todo)
    {
        return Ok();
    }
    
    [HttpPost("delete-todo")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}