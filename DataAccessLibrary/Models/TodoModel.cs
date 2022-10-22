using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    /// <summary>
    /// Represents the Todo data model
    /// </summary>
    public class TodoModel
    {
        [BsonId]
        public Guid Id { get; } = Guid.NewGuid();

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("isComplete")]
        public bool IsComplete { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; } = DateTime.Now;

        [BsonElement("updated")]
        public DateTime Updated { get; set; } = DateTime.Now;

    }
}
