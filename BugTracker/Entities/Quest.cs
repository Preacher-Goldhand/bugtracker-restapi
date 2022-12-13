using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Entities
{
    public class Quest
    {
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

        public int? AssignerId { get; set; }

        [ForeignKey("AssignerId")]
        public virtual Employee Assigner { get; set; }

        public int? AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public virtual Employee Assignee { get; set; }

        public int BoardId { get; set; }

        public Board Board { get; set; }
    }
}