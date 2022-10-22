using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary;
using DataAccessLibrary.Models;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<TodoModel>> GetAll()
    {
        var todos = _db.LoadRecords<TodoModel>(_table);
        if (todos is null) return NotFound();
        return Ok(todos);

    }
}

