using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;

namespace TodoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly TodoService _todoService;

    public TodosController(TodoService todoService, ILogger<TodosController> logger)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<List<TodoModel>> GetAllTodos()
    {
        return await _todoService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById([FromRoute] Guid id)
    {
        var todo =  await _todoService.GetByIdAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] TodoModel todo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model");
        }

        todo.Initalize();

        await _todoService.InsertTodo(todo);

        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTodo()
    {
        return NotFound();
    }
}

