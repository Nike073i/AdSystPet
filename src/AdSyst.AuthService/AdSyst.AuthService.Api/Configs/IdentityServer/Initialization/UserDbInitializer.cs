using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Api.Configs.Consts;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.AuthService.EfContext.UserData.Contexts;
using AdSyst.Common.Presentation.Data;

namespace AdSyst.AuthService.Api.Configs.IdentityServer.Initialization
{
    public class UserDbInitializer : IDbInitializer
    {
        private readonly InitializeUserData _systemData;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserDataDbContext _dbContext;

        public UserDbInitializer(
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            UserDataDbContext dbContext
        )
        {
            _systemData = configuration
                .GetRequiredSection(ConfigurationKeys.SystemUserDataSectionKey)
                .Get<InitializeUserData>()!;

            _userManager = userManager;
            _dbContext = dbContext;
        }

        public bool Initialize()
        {
            _dbContext.Database.Migrate();

            if (_userManager.Users.Any())
                return false;
            SeedUser(_systemData, Role.System);
            return true;
        }

        private void SeedUser(InitializeUserData userData, Role role)
        {
            var user = new AppUser(userData.FirstName, userData.LastName, userData.Birthday)
            {
                UserName = userData.UserName,
                Email = userData.Email,
                EmailConfirmed = true,
            };
            var result = _userManager.CreateAsync(user, userData.Password).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new DataInitializationException(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }
            _userManager.AddToRoleAsync(user, role.ToString()).GetAwaiter().GetResult();
        }
    }
}
