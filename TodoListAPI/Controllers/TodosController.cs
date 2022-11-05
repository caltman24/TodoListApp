using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;
using MySql.Data.MySqlClient;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TodoModel>))]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await _todoService.GetAllAsync();

        if (todos == null)
        {
            return NotFound();
        }

        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoModel))]
    public async Task<IActionResult> GetTodoById([FromRoute] int id)
    {
        var todo = await _todoService.GetByIdAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTodo([FromBody] TodoModel todo)
    {
        // TODO: Return the inserted record from the database

        if (todo.UserId <= 0)
        {
            return BadRequest($"Invalid userId. Value must be greater than 0. Got {todo.UserId} instead");
        }

        try
        {
            await _todoService.InsertAsync(todo);
            return Ok();

        } catch (MySqlException ex)
        {
            _logger.LogError("There was an error in the CreateTodo action. Error: {ex}", ex);
            return StatusCode(500, "There was a problem creating the todo. Check if user id exists");
        }
        
    }
}

