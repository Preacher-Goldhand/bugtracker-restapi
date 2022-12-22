using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Entities
{
    public class Employee
    {
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

        public string EmployeePasswordHash { get; set; }

        public int RoleId { get; set; } = 1;
        public virtual Role Role { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }

        // Must be, but needs improvements
        public virtual List<Quest> AssignerTasks { get; set; } = new List<Quest>();

        public virtual List<Quest> AssigneeTasks { get; set; } = new List<Quest>();
    }
}