using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;
using MySql.Data.MySqlClient;

namespace TodoListAPI.Controllers;

//TODO: Check if userId exists before performing any operations and return an error

[Route("api/{userId}/[controller]")]
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
    public async Task<IActionResult> GetAllTodos([FromRoute] int userId)
    {
        var todos = await _todoService.GetAllAsync(userId);

        if (todos == null)
        {
            return NotFound();
        }

        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoModel))]
    public async Task<IActionResult> GetTodoById([FromRoute] int userId, [FromRoute] int id)
    {
        var todo = await _todoService.GetByIdAsync(id, userId);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTodo([FromBody] TodoModel todo, [FromRoute] int userId)
    {
        // TODO: Return the inserted record from the database

        if (userId <= 0)
        {
            return BadRequest($"Invalid userId. Value must be greater than 0. Got {userId} instead");
        }

        try
        {
            await _todoService.InsertAsync(todo, userId);
            _logger.LogInformation("Todo Created: {title}", todo.Title);
            return Ok();

        } catch (MySqlException ex)
        {
            _logger.LogError("There was an error in the CreateTodo action. Error: {ex}", ex);
            return StatusCode(500, "There was a problem creating the todo. Check if user id exists");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CompleteTodo([FromRoute] int userId,[FromRoute] int id, [FromQuery] bool isComplete)
    {
        await _todoService.CompleteByIdAsync(id, userId, isComplete);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] int userId, [FromRoute] int id)
    {
        await _todoService.DeleteByIdAsync(id, userId);
        return Ok();
    }
}

