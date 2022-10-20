using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    /// <summary>
    /// MongoDB data access layer
    /// </summary>
    public class MongoDBDataAccess
    {
        private readonly IMongoDatabase _db;

        /// <summary>
        /// Initalize a new instance of MongoDBDataAccess
        /// </summary>
        /// <param name="dbName">name of database</param>
        /// <param name="connectionString">connection string to database</param>
        public MongoDBDataAccess(string dbName, string connectionString)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(dbName);
        }

        /// <summary>
        /// Insert a record into a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">name of collection</param>
        /// <param name="record">record to insert</param>
        public void InsertRecord<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        /// <summary>
        /// Loads records from a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">name of collection</param>
        /// <returns>A list of records</returns>
        public List<T>? LoadRecords<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Load a record by Id from a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">name of collection</param>
        /// <param name="id">Id of record</param>
        /// <returns>A record from the collection</returns>
        public T? LoadRecordById<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
