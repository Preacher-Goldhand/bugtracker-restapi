using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.AuthenticationDtos
{
    public class LoginEmployeeDto
    {
        [Required]
        public string EmployeeEmail { get; set; }

        [Required]
        public string EmployeePasswordHash { get; set; }
    }
}