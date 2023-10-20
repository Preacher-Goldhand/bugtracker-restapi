using System.ComponentModel.DataAnnotations;

namespace BugTracker.Entities
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime PropsalDate { get; set; }
        public float LoggedTime { get; set; }
        public int Priority { get; set; }

        [Required]
        [MaxLength(20)]
        public string TaskStatus { get; set; }

        public int AssignerId { get; set; }

        public virtual Employee Assigner { get; set; }

        public int AssigneeId { get; set; }

        public virtual Employee Assignee { get; set; }

        public int BoardId { get; set; }

        public virtual Board Board { get; set; }
    }
}