namespace BugTracker.Models.AuthenticationDtos
{
	public class AccountDetailsEmployee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmployeeEmail { get; set; }

        public int RoleId { get; set; }

        public string Token { get; set; }
    }
}

