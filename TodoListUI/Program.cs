using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;

MongoDBDataAccess db = new("TodoListApp", GetConnectionString());
string collectionName = "todos";

TodoModel todo = new()
{
    Title = "dishes",
    Created = DateTime.Now,
    IsComplete = true,
};

CreateTodo(todo);

ReadTodos();

Console.ReadLine();

void CreateTodo(TodoModel todo)
{
    db.UpsertRecord(collectionName, todo, todo.Id);
    Console.WriteLine($"Todo created: {todo.Title} {todo.Created}");
}

void ReadTodos()
{
    var todos = db.LoadRecords<TodoModel>(collectionName);

    if (todos is null || todos.Count <= 0) Console.WriteLine("No Todos");
    todos?.ForEach(t =>
    {
        Console.WriteLine($"{t.Title}");
    });
}

static string GetConnectionString(string name = "Default")
{
    var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

    var config = builder.Build();

    return config.GetConnectionString(name);
}

