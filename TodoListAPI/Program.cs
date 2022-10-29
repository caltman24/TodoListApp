using Microsoft.Net.Http.Headers;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure MongoDBSettings and get the section containing the info
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB")
);
// Add TodosService to the dependencies for injection
builder.Services.AddSingleton<TodoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Logging to the container
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();

app.Run();
