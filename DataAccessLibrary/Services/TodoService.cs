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

        public async Task<List<TodoModel>> GetAllAsync(int userId)
        {
            string sql = "dbo.get_all_todos;";

            // TODO: Check if user exists

            var todos = await _db.LoadData<TodoModel, dynamic>(sql, new { userId });
            return todos.ToList();
        }

        public async Task<TodoModel?> GetByIdAsync(int id, int userId)
        {
            string sql = "dbo.get_todo_by_id";

            var queryResult = await _db.LoadData<TodoModel, dynamic>(sql, new { id, userId });
            return queryResult.FirstOrDefault();
        }

        public async Task InsertAsync(TodoModel todo, int userId)
        {
            string sql = "dbo.create_todo";
            var queryParams = new { todo.Title, todo.Description, todo.IsComplete, userId};

            // TODO: Return the inserted record from the database
            await _db.SaveData(sql, queryParams);
        }

        public async Task CompleteByIdAsync(int id, int userId, bool isComplete)
        {
            string sql = "dbo.complete_todo";
            var queryParams = new { id, userId, isComplete };

            await _db.SaveData(sql, queryParams);
        }

        public async Task UpdateByIdAsync(int id, int userId, string title, string description)
        {
            string sql = "dbo.update_todo";
            var queryParams = new { id, userId, title, description };

            await _db.SaveData(sql, queryParams);
        }

        public async Task DeleteByIdAsync(int id, int userId)
        {
            string sql = "dbo.delete_todo";
            var queryParams = new { id, userId };

            await _db.SaveData(sql, queryParams);
        }

    }
}
