namespace AdSyst.AuthService.Application.Users.Queries.IsUsernameFree
{
    public interface IIsUsernameFreeService
    {
        Task<bool> CheckAsync(string userName, CancellationToken cancellationToken = default);
    }
}
