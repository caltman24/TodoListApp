using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.Services
{
    /// <summary>
    /// This class is responsible for executing CRUD against the data access layer
    /// </summary>
    public class TodoService
    {
        private readonly MySQLDataAccess _db;

        public TodoService(IConfiguration config)
        {
            _db = new(config.GetConnectionString("Dev"));
        }

        public async Task<List<TodoModel>> GetAllAsync()
        {
            string sql = "dbo.get_all_todos;";

            var todos = await _db.LoadData<TodoModel, dynamic>(sql, new { });
            return todos.ToList();
        }

        public async Task<TodoModel?> GetByIdAsync(int id)
        {
            string sql = "dbo.get_todo_by_id";

            var queryResult = await _db.LoadData<TodoModel, dynamic>(sql, new { id });
            return queryResult.FirstOrDefault();
        }

        public async Task InsertAsync(TodoModel todo)
        {
            string sql = "dbo.create_todo";
            var queryParams = new { todo.Title, todo.Description, todo.IsComplete, todo.UserId };

            // TODO: Return the inserted record from the database
            await _db.SaveData(sql, queryParams);
        }

        public async Task CompleteByIdAsync(int id, bool isComplete)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateByIdAsync(int id, string title)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
