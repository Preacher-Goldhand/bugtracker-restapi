using System.ComponentModel.DataAnnotations;

namespace BugTracker.Entities
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public virtual List<Quest> BoardTasks { get; set; } = new List<Quest>();
    }
}