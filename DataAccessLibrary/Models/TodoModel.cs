using System.ComponentModel.DataAnnotations;


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
        bool? IsComplete
    )
    {
        // To use records with Dapper. You have to declare a parameterless constructor
        public TodoModel() : this(default, default, default, default) { }
    };

}
