using DataAccessLibrary.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoModel> _todosCollection;

        // On initialization, we take in mongoDBsettings which is contained in "appsettings.json".
        // This containts the ConnectionURI, databaseName, and collectionName
        // This may get injected via dependency injection
        public TodoService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            // Initialize a new instance of a MongoClient using the connectionURI
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionURI);

            // Get the instance of the database using the databaseName
            IMongoDatabase database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            // Get the collection(of TodoModel) from the database using the connectionName
            _todosCollection = database.GetCollection<TodoModel>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<TodoModel>> GetAllAsync()
        {
            return await _todosCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
