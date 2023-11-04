using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Entities
{
    public class TaskComment
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        [Required]
        public int UserCreatedId { get; set; }

        public virtual Employee UserCreated { get; set; }

        [MaxLength(200)]
        public string? FileName { get; set; }

        [Required]
        public int TaskId { get; set; }
        public Quest? Quest { get; set; }
    }
}

