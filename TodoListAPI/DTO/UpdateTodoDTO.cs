using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.DTO
{
    public class UpdateTodoDTO
    {
        [BsonElement("title")]
        [MaxLength(50)]
        public string? Title { get; set; }

        [BsonElement("isComplete")]
        public bool? IsComplete { get; set; }

    }
}
