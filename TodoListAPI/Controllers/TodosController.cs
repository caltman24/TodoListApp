using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace TodoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger _logger;

    public TodosController(IConfiguration config, ILogger<TodosController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllTodos()
    {
        return NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult GetTodoById([FromRoute] Guid id)
    {
        return NotFound();
    }

    [HttpPost]
    public IActionResult CreateTodo()
    {
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTodo()
    {
        return NotFound();
    }
}

