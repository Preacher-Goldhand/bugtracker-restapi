using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class MinimalQuestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        public int Priority { get; set; }

        [Required]
        [MaxLength(20)]
        public string TaskStatus { get; set; }
    }
}