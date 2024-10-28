using Microsoft.AspNetCore.Identity;

namespace AdSyst.AuthService.Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Birthday { get; private set; }

        public AppUser(string firstName, string lastName, DateTimeOffset birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }
    }
}
