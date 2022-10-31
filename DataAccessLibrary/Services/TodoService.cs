using DataAccessLibrary.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Services
{
    /// <summary>
    /// This class is responsible for executing CRUD opertations against the TodoList DB
    /// </summary>
    public class TodoService
    {
        private readonly IMongoCollection<TodoModel> _todosCollection;

        // On initialization, we take in mongoDBsettings which is contained in "appsettings.json".
        // This containts the ConnectionURI, databaseName, and collectionName
        // This may get injected via dependency injection
        public TodoService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _todosCollection = database.GetCollection<TodoModel>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<TodoModel>> GetAllAsync()
        {
            return await _todosCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TodoModel> GetByIdAsync(Guid id)
        {
            return await _todosCollection.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertTodoAsync(TodoModel todo)
        {
            todo.Initalize();
            await _todosCollection.InsertOneAsync(todo);
        }

        public async Task CompleteTodoAsync(Guid id, bool isComplete)
        {
            FilterDefinition<TodoModel> filter = Builders<TodoModel>.Filter.Eq("Id", id);
            UpdateDefinition<TodoModel> update = Builders<TodoModel>.Update
                .Set(d => d.IsComplete, isComplete);

            await _todosCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTodoAsync(Guid id, string title)
        {
            FilterDefinition<TodoModel> filter = Builders<TodoModel>.Filter.Eq("Id", id);
            UpdateDefinition<TodoModel> update = Builders<TodoModel>.Update
                .Set(d => d.Title, title);

            await _todosCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteTodoByIdAsync(Guid id)
        {
            await _todosCollection.DeleteOneAsync(d => d.Id == id);
        }

    }
}
