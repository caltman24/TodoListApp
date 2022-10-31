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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<TodoModel>>> GetAllTodos()
    {
        var todos =  await _todoService.GetAllAsync();

        if (todos == null)
        {
            return NotFound();
        }

        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodo([FromBody] TodoModel todo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model");
        }

        await _todoService.InsertTodoAsync(todo);
        _logger.LogInformation("Todo Created: {id}", todo.Id);

        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}/Complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CompleteTodo([FromRoute] Guid id, [FromBody] bool isComplete)
    {
        await _todoService.CompleteTodoAsync(id, isComplete);
        _logger.LogInformation("Todo Completed: {id} - {complete}", id, isComplete);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTodoTitle([FromRoute] Guid id, [FromBody] string title)
    {
        await _todoService.UpdateTodoAsync(id, title);
        _logger.LogInformation("Todo Updated: {id} - {title}", id, title);
        return Ok();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
    {
        await _todoService.DeleteTodoByIdAsync(id);
        _logger.LogInformation("Todo Deleted: {id}", id);
        return Ok();
    }
}

