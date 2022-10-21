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
        /// <summary>
        /// Unique Identifer for todo
        /// </summary>
        [BsonId]
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Title of todo
        /// </summary>
        [BsonElement("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Determine if todo is complete
        /// </summary>
        [BsonElement("isComplete")]
        public bool IsComplete { get; set; }

        /// <summary>
        /// Date and time todo was created
        /// </summary>
        [BsonElement("created")]
        public DateTime Created { get; set; }

    }
}
