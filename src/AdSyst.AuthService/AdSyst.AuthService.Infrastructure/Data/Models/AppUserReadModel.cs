namespace AdSyst.AuthService.EfContext.UserData.Models
{
    public class AppUserReadModel
    {
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string NormalizedUserName { get; private set; }
        public string Email { get; private set; }
        public string NormalizedEmail { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTimeOffset Birthday { get; private set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private AppUserReadModel() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public DateTimeOffset? LockoutEnd { get; private set; }

        public bool IsActive => LockoutEnd == null || LockoutEnd <= DateTimeOffset.Now;

        public bool EmailConfirmed { get; private set; }

        public HashSet<UserRoleReadModel>? UserRoles { get; private set; }
    }
}
