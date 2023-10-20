using System.ComponentModel.DataAnnotations;
using BugTracker.Entities;

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

        public int AssignerId { get; set; }

        public virtual EmployeeDto Assigner { get; set; }

        public int AssigneeId { get; set; }

        public virtual EmployeeDto Assignee { get; set; }
    }
}