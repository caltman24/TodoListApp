using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System.Collections.Generic;

namespace TodoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IMongoDBDataAccess _db;
    private readonly ILogger _logger;
    private readonly string _table = "todos";


    public TodosController(IConfiguration config, ILogger<TodosController> logger)
    {
        _config = config;
        _db = new MongoDBDataAccess("TodoListApp", _config.GetConnectionString("Default"));
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(
        StatusCodes.Status200OK,
        Type = typeof(IEnumerable<TodoModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAllTodos()
    {
        var todos = _db.LoadRecords<TodoModel>(_table);
        if (todos is null) return NotFound();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetTodoById([FromRoute] Guid id)
    {
        var todo = _db.LoadRecordById<TodoModel>(_table, id);
        if (todo == null)
        {
            return BadRequest("Todo object is null");
        }
        return Ok(todo);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // Fix: error handling and model validation
    // Fix: we should not insert a record into the db if the model is invalid
    public IActionResult CreateTodo([FromBody] TodoModel todo)
    {
        try
        {
            todo.Initalize();
            var newTodo = _db.UpsertRecord(_table, todo, todo.Id);

            if (newTodo == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            return CreatedAtRoute("TodoById", new { id = newTodo.Id }, newTodo);
        }
        catch(Exception ex)
        {
            _logger.LogError("Something went wrong insde the CreateTodo action: {ex}", ex);
            return StatusCode(500, "Internal server error");
        }
        
    }
}

