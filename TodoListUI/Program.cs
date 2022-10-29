using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;

IMongoDBDataAccess db = new MongoDBDataAccess("TodoListApp", GetConnectionString());
string collectionName = "todos";

TodoModel todo1 = new()
{
    Title = "dishes",
    IsComplete = true,
};

TodoModel todo2 = new()
{
    Title = "homework",
    IsComplete = false,
};

CreateTodo(todo1);
CreateTodo(todo2);
ReadTodos();

todo2.Title = "Eat some pizza";
UpdateTodo(todo2);

DeleteTodo(todo1);

Console.ReadLine();

void CreateTodo(TodoModel todo)
{
    db.UpdateRecord(collectionName, todo, todo.Id);
    Console.WriteLine($"Todo created: {todo.Title} {todo.Created}");
}

void DeleteTodo(TodoModel todo)
{
    db.DeleteRecordById<TodoModel>(collectionName, todo.Id);
    Console.WriteLine($"Todo Deleted: {todo.Title}");
}

void UpdateTodo(TodoModel todo)
{
    todo.Updated = DateTime.Now;
    db.UpdateRecord(collectionName, todo, todo.Id);
    Console.WriteLine($"Todo updated: {todo.Title}");
}

void ReadTodos()
{
    var todos = db.LoadRecords<TodoModel>(collectionName);

    if (todos is null || todos.Count <= 0)
    {
        Console.WriteLine("No Todos");
        return;
    }

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

