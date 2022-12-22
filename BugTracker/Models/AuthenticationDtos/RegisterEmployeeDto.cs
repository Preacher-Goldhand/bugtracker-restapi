using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.AuthenticationDtos
{
    public class RegisterEmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeePasswordHash { get; set; }
        public string ConfirmEmployeePaswordHash { get; set; }

        public int RoleId { get; set; } = 1;

        public string EmployeePhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }
    }
}