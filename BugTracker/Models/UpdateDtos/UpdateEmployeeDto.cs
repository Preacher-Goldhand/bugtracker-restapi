using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Models.UpdateDtos
{
    public class UpdateEmployeeDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string Department { get; set; }

        [MaxLength(30)]
        public string EmployeeEmail { get; set; }

        public int AvailableHours { get; set; }

        public int RoleId { get; set; }

        public string EmployeePhoneNumber { get; set; }

        [MaxLength(20)]
        public string EmployeeStatus { get; set; }
    }
}