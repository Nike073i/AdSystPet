namespace AdSyst.AuthService.EfContext.UserData.Models
{
    public class UserRoleReadModel
    {
        public AppUserReadModel User { get; private set; }
        public string UserId { get; private set; }

        public RoleReadModel Role { get; private set; }
        public string RoleId { get; private set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private UserRoleReadModel() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    }
}
