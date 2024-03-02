using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.AuthenticationDtos
{
    public class ChangePasswordDto
    {
        public string EmployeeEmail { get; set; }
        public string CurrentPasswordHash { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
        public string NewPasswordHash { get; set; }
    }
}