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
    private readonly string _table = "todos";

    public TodosController(IConfiguration config)
    {
        _config = config;
        _db = new MongoDBDataAccess("TodoListApp", _config.GetConnectionString("Default"));
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

    [HttpGet("{id}", Name ="TodoById")]
    public IActionResult GetTodoById(Guid id)
    {
        return Ok();
    }


    [HttpPost]
    // Todo: use a DTO instead of TodoModel
    public IActionResult CreateTodo([FromBody] TodoModel todo)
    {
        _db.UpsertRecord(_table, todo, todo.Id);
        return Ok();
    }
}

