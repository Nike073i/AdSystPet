namespace AdSyst.AuthService.Api.Configs.IdentityServer.Initialization
{
    public class InitializeUserData
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset Birthday { get; set; }
    }
}
