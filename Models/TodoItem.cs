using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoList.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        [DisplayName("Is Complete")]
        public Boolean IsComplete { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DisplayName("Difficulty Level")]
        [Range(0, 100, ErrorMessage = "The Difficulty Level must be between 0 and 100!")]
        public int DifficultyLevel { get; set; }

    }
}
