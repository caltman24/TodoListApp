using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Guid Id { get; private set; }

        [BsonElement("title")]
        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [BsonElement("isComplete")]
        [Required]
        public bool? IsComplete { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; private set; }

        [BsonElement("updated")]
        public DateTime Updated { get; private set; }

        public void Initalize()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public void UpdateTime()
        {
            Updated = DateTime.Now;
        }
    }
}
