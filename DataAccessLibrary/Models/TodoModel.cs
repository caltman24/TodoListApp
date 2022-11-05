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
    public record TodoModel
    (
        int? Id,

        [Required]
        [MaxLength(50)]
        string? Title,

        [MaxLength(500)]
        string? Description,

        [Required]
        bool? IsComplete,

        int? UserId
    )
    {
        // To use records with Dapper. You have to declare a parameterless constructor
        public TodoModel() : this(default, default, default, default, default) { }
    };

}
