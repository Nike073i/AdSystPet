namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails
{
    public interface IGetUserInfoDetailsService
    {
        Task<UserInfoDetailDto?> GetAsync(
            string userId,
            CancellationToken cancellationToken = default
        );
    }
}
