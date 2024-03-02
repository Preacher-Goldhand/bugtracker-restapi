using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Department { get; set; }

        [Required]
        [MaxLength(30)]
        public string EmployeeEmail { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
        public string EmployeePasswordHash { get; set; }
        public int AvailableHours { get; set; }

        public int RoleId { get; set; } = 1;
        public virtual Role Role { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }

        // Must be, but needs improvements
        [NotMapped]
        public virtual List<Quest> AssignerTasks { get; set; } = new List<Quest>();

        [NotMapped]
        public virtual List<Quest> AssigneeTasks { get; set; } = new List<Quest>();
    }
}