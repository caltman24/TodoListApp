using Microsoft.Net.Http.Headers;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:3000");
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Add Dependencies
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

app.UseCors();

app.UseAuthorization();


app.MapControllers();

app.Run();
