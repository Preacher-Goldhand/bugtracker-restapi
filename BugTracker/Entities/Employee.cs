using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }

        // Future features of the application
        public virtual List<Quest> AssignerTasks { get; set; } = new List<Quest>();

        public virtual List<Quest> AssigneeTasks { get; set; } = new List<Quest>();
    }
}