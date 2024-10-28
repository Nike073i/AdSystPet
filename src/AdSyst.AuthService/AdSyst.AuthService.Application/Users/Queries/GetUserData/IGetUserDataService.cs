namespace AdSyst.AuthService.Application.Users.Queries.GetUserData
{
    public interface IGetUserDataService
    {
        Task<UserDataDto?> GetAsync(string userId, CancellationToken cancellationToken = default);
    }
}
